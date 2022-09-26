#pragma once

#include <Windows.h>
#include <Winternl.h>
#include <stdio.h>

// Error Codes
enum DBG_CATCH
{
	DBG_NONE = 0x0000,

	// Memory Codes (0x1000 range)
	DBG_BEINGEBUGGEDPEB = 0x1000,
	DBG_CHECKREMOTEDEBUGGERPRESENT = 0x1001,
	DBG_ISDEBUGGERPRESENT = 0x1002,
	DBG_NTGLOBALFLAGPEB = 0x1003,
	DBG_NTQUERYINFORMATIONPROCESS = 0x1004,
	DBG_FINDWINDOW = 0x1005,
	DBG_OUTPUTDEBUGSTRING = 0x1006,
	DBG_NTSETINFORMATIONTHREAD = 0x1007,
	DBG_DEBUGACTIVEPROCESS = 0x1008,

	// CPU Codes (0x2000 range)
	DBG_HARDWAREDEBUGREGISTERS = 0x2000,
	DBG_MOVSS = 0x2001,

	// Timing Codes (0x3000 range)
	DBG_RDTSC = 0x3000,
	DBG_QUERYPERFORMANCECOUNTER = 0x3001,
	DBG_GETTICKCOUNT = 0x3002,

	// Exception Codes (0x4000 range)
	DBG_CLOSEHANDLEEXCEPTION = 0x4000,
	DBG_SINGLESTEPEXCEPTION = 0x4001,
	DBG_INT3CC = 0x4002,
	DBG_PREFIXHOP = 0x4003,

} DBG_CATCH;

// Debugging messages
void DBG_MSG(WORD dbg_code, char* message);

// Dynamically resolved functions
typedef NTSTATUS(__stdcall* _NtQueryInformationProcess)(_In_ HANDLE, _In_  unsigned int, _Out_ PVOID, _In_ ULONG, _Out_ PULONG);
typedef NTSTATUS(__stdcall* _NtSetInformationThread)(_In_ HANDLE, _In_ THREAD_INFORMATION_CLASS, _In_ PVOID, _In_ ULONG);

#include <Windows.h>
#include "antidbg.h"

#define SHOW_DEBUG_MESSAGES

// =======================================================================
// Debugging helper
// =======================================================================
void DBG_MSG(WORD dbg_code, char* message)
{
#ifdef SHOW_DEBUG_MESSAGES
	printf("[MSG-0x%X]: %s\n", dbg_code, message);
	MessageBoxA(NULL, message, "GAME OVER!", 0);
#endif
}

// =======================================================================
// Memory Checks
// These checks focus on Windows structures containing information which 
// can reveal the presence of a debugger. 
// =======================================================================

/*
 * // adbg_BeingDebuggedPEB()
 *
 * // How it works:
 * Checks the Process Environment Block (PEB) for a "BeingDebugged"
 * field which is set when the process launches under a debugger. This
 * method is exactly what IsDebuggerPresent() checks under the hood,
 * it is simply the assembly version of this call.
 *
 * // Indication:
 * Look for PEB references.
 * These references typically start with FS:[0x30h]. FS stands for
 * "Frame Segment" and generally indicates references to an application's
 * own internal header structures. These should not raise red flags,
 * however they should be noted.
 *
 * // Bypass:
 * Once the BeingDebugged byte in the PEB is queried, flip the value
 * from 1 to 0 before it is evaluated by the application logic.
 */
void adbg_BeingDebuggedPEB(void)
{
	BOOL found = FALSE;
	_asm
	{
		xor eax, eax;			// clear eax
		mov eax, fs: [0x30] ;		// Reference start of the PEB
		mov eax, [eax + 0x02];	// PEB+2 points to BeingDebugged
		and eax, 0x000000FF;	// only reference one byte
		mov found, eax;			// Copy BeingDebugged into 'found'
	}

	if (found)
	{

		exit(DBG_BEINGEBUGGEDPEB);
	}
}


/*
 * // adbg_CheckRemoteDebuggerPresent()
 *
 * // How it works:
 * ...
 *
 * // Indication:
 * Look for this imported function or calls to GetProcAddress().
 * CheckRemoteDebuggerPresent is similar to IsDebuggerPresent,
 * except. It allows an applicaion to query the debugging state of
 * another application via a process handle. The BOOL return value
 * is used to determine if the process (hProcess) is being debugged.
 *
 * // Bypass:
 * Set a breakpoint on CheckRemoteDebuggerPresent(), single step,
 * then switch the return value to 0.
 */
void adbg_CheckRemoteDebuggerPresent(void)
{
	HANDLE hProcess = INVALID_HANDLE_VALUE;
	BOOL found = FALSE;

	hProcess = GetCurrentProcess();
	CheckRemoteDebuggerPresent(hProcess, &found);

	if (found)
	{

		exit(DBG_CHECKREMOTEDEBUGGERPRESENT);
	}
}



/*
 * // adbg_IsDebuggerPresent()
 *
 * // How it works:
 * Checks the PEB structure for the value of BeingDebugged.
 *
 * // Indication:
 * Look for this imported function or calls to GetProcAddress().
 * IsDebuggerPresent is exported from kernel32.dll. The BOOL return value
 * is used to determine if an application is being debugged.
 *
 * // Bypass:
 * Set a breakpoint on IsDebuggerPresent(), single step, then
 * switch the return value to 0.
 */
void adbg_IsDebuggerPresent(void)
{
	BOOL found = FALSE;
	found = IsDebuggerPresent();

	if (found)
	{

		exit(DBG_ISDEBUGGERPRESENT);
	}
}


/*
 * // adbg_NtGlobalFlagPEB()
 *
 * // How it works:
 *
 *
 * // Indication:
 * Look for Process Environment Block (PEB) references.
 * These references typically start with FS:[0x30h]. FS stands for
 * "Frame Segment" and generally indicates references to an application's
 * own internal header structures. These should not raise red flags,
 * however they should be noted. 0x68 offset from the PEB is the
 * NtGlobalFlag value. When a process is being debugged, three flags
 * are set, FLG_HEAP_ENABLE_TAIL_CHECK (0x10), FLG_HEAP_ENABLE_FREE_CHECK
 * (0x20), and FLG_HEAP_VALIDATE_PARAMETERS (0x40).
 *
 * // Bypass:
 * ...
 */
void adbg_NtGlobalFlagPEB(void)
{
	BOOL found = FALSE;
	_asm
	{
		xor eax, eax;			// clear eax
		mov eax, fs: [0x30] ;		// Reference start of the PEB
		mov eax, [eax + 0x68];	// PEB+0x68 points to NtGlobalFlags
		and eax, 0x00000070;	// check three flags
		mov found, eax;			// Copy result into 'found'
	}

	if (found)
	{

		exit(DBG_NTGLOBALFLAGPEB);
	}
}






/*
* // adbg_DebugActiveProcess()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*/
void adbg_DebugActiveProcess(const char* cpid)
{
	BOOL found = FALSE;
	STARTUPINFOA si = { 0 };
	PROCESS_INFORMATION pi = { 0 };
	si.cb = sizeof(si);
	TCHAR szPath[MAX_PATH];
	DWORD exitCode = 0;

	CreateMutex(NULL, FALSE, (LPCWSTR)"antidbg");
	if (GetLastError() != ERROR_SUCCESS)
	{
		// If we get here we are in the child process
		if (DebugActiveProcess((DWORD)atoi(cpid)))
		{
			// No debugger found.
			return;
		}
		else
		{
			// Debugger found, exit child with a unique code we can check for.
			exit(555);
		}
	}

	// parent process
	DWORD pid = GetCurrentProcessId();
	GetModuleFileName(NULL, szPath, MAX_PATH);

	char cmdline[MAX_PATH + 1 + sizeof(int)];
	snprintf(cmdline, sizeof(cmdline), "%ws %d", szPath, pid);

	// Start the child process. 
	BOOL success = CreateProcessA(
		NULL,		// path (NULL means use cmdline instead)
		cmdline,	// Command line
		NULL,		// Process handle not inheritable
		NULL,		// Thread handle not inheritable
		FALSE,		// Set handle inheritance to FALSE
		0,			// No creation flags
		NULL,		// Use parent's environment block
		NULL,		// Use parent's starting directory 
		&si,		// Pointer to STARTUPINFO structure
		&pi);		// Pointer to PROCESS_INFORMATION structure

	// Wait until child process exits and get the code
	WaitForSingleObject(pi.hProcess, INFINITE);

	// Check for our unique exit code
	GetExitCodeProcess(pi.hProcess, &exitCode);
	if (exitCode == 555)
	{
		found = TRUE;
	}

	// Close process and thread handles. 
	CloseHandle(pi.hProcess);
	CloseHandle(pi.hThread);

	if (found)
	{

		exit(DBG_DEBUGACTIVEPROCESS);
	}
}

// =======================================================================
// Timing Checks
// These checks focus on comparison of time stamps between a portion
// of code which is likely to be analyzed under a debugger. The goal
// is to determine with high probability that a debugger is allowing
// single step control, or that a breakpoint had been hit between
// the time check locations.
// =======================================================================

/*
 * // adbg_RDTSC()
 *
 * // How it works:
 * ...
 *
 * // Indication:
 * ...
 *
 * // Bypass:
 * ...
 */
void adbg_RDTSC(void)
{
	BOOL found = FALSE;

	UINT64 timeA, timeB = 0;
	int timeUpperA, timeLowerA = 0;
	int timeUpperB, timeLowerB = 0;

	_asm
	{
		// rdtsc stores result across EDX:EAX
		rdtsc;
		mov timeUpperA, edx;
		mov timeLowerA, eax;

		// Junk code to entice stepping through or a breakpoint
		xor eax, eax;
		mov eax, 5;
		shr eax, 2;
		sub eax, ebx;
		cmp eax, ecx

			rdtsc;
		mov timeUpperB, edx;
		mov timeLowerB, eax;
	}

	timeA = timeUpperA;
	timeA = (timeA << 32) | timeLowerA;

	timeB = timeUpperB;
	timeB = (timeB << 32) | timeLowerB;

	/* 0x10000 is purely empirical and is based on the computer's clock cycle
	   This value should be change depending on the length and complexity of
	   code between each RDTSC operation. */
	if (timeB - timeA > 0x10000)
	{
		found = TRUE;
	}

	if (found)
	{

		exit(DBG_RDTSC);
	}
}


/*
* // adbg_QueryPerformanceCounter()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*/
void adbg_QueryPerformanceCounter(void)
{
	BOOL found = FALSE;
	LARGE_INTEGER t1;
	LARGE_INTEGER t2;

	QueryPerformanceCounter(&t1);

	// Junk or legit code.
	_asm
	{
		xor eax, eax;
		push eax;
		push ecx;
		pop eax;
		pop ecx;
		sub ecx, eax;
		shl ecx, 4;
	}

	QueryPerformanceCounter(&t2);

	// 30 is an empirical value
	if ((t2.QuadPart - t1.QuadPart) > 30)
	{
		found = TRUE;
	}

	if (found)
	{

		exit(DBG_QUERYPERFORMANCECOUNTER);
	}
}


/*
* // adbg_RDTSC()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*/
void adbg_GetTickCount(void)
{
	BOOL found = FALSE;
	DWORD t1;
	DWORD t2;

	t1 = GetTickCount();

	// Junk or legit code.
	_asm
	{
		xor eax, eax;
		push eax;
		push ecx;
		pop eax;
		pop ecx;
		sub ecx, eax;
		shl ecx, 4;
	}

	t2 = GetTickCount();

	// 30 milliseconds is an empirical value
	if ((t2 - t1) > 30)
	{
		found = TRUE;
	}

	if (found)
	{

		exit(DBG_GETTICKCOUNT);
	}
}


// =======================================================================
// CPU Checks
// These checks focus on aspects of the CPU, including hardware break-
// points, special interrupt opcodes, and flags.
// =======================================================================

/*
 * // adbg_HardwareDebugRegisters()
 *
 * // How it works:
 * ...
 *
 * // Indication:
 * ...
 *
 * // Bypass:
 * ...
 */
void adbg_HardwareDebugRegisters(void)
{
	BOOL found = FALSE;
	CONTEXT ctx = { 0 };
	HANDLE hThread = GetCurrentThread();

	ctx.ContextFlags = CONTEXT_DEBUG_REGISTERS;
	if (GetThreadContext(hThread, &ctx))
	{
		if ((ctx.Dr0 != 0x00) || (ctx.Dr1 != 0x00) || (ctx.Dr2 != 0x00) || (ctx.Dr3 != 0x00) || (ctx.Dr6 != 0x00) || (ctx.Dr7 != 0x00))
		{
			found = TRUE;
		}
	}

	if (found)
	{

		exit(DBG_HARDWAREDEBUGREGISTERS);
	}
}


/*
* // adbg_MovSS()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*/

// =======================================================================
// Exception Checks
// These checks focus on exceptions that occur when under the control of 
// a debugger. In several cases, there are certain exceptions that will
// be thrown only when running under a debugger.
// =======================================================================

/*
 * // adbg_CloseHandleException()
 *
 * // How it works:
 * CloseHandle will throw an exception when trying to close an
 * invalid handle, only when running under a debugger. We pass
 * an invalid handle into CloseHandle to force an exception,
 * where our own exception handler will close the application.
 *
 * // Indication:
 * Look for possibly invalid handles passed to CloseHandle().
 * The validity of a handle can be difficult to assess, but
 * an application closing shortly after CloseHandle is a great
 * indication.
 *
 * // Bypass:
 * Modify the invalid handle passed into CloseHandle()
 * to be INVALID_HANDLE_VALUE, patch the call, or adjust EIP to
 * skip over the invalid CloseHandle. This may be easier said than
 * done if the CloseHandle is called many times with a mix of
 * valid and invalid handles.
 *
 */
void adbg_CloseHandleException(void)
{
	HANDLE hInvalid = (HANDLE)0xDEADBEEF; // an invalid handle
	DWORD found = FALSE;

	__try
	{
		CloseHandle(hInvalid);
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		found = TRUE;
	}

	if (found)
	{

		exit(DBG_CLOSEHANDLEEXCEPTION);
	}
}


/*
 * // adbg_SingleStepException()
 *
 * // How it works:
 * ...
 *
 * // Indication:
 * ...
 *
 * // Bypass:
 * ...
 *
 */
void adbg_SingleStepException(void)
{
	DWORD found = TRUE;

	/*
	In this method we force an exception to occur. If it occurs
	outside of a debugger, the __except() handler is called setting
	found to FALSE. If the exception occurs inside of a debugger, the
	__except() will not be called (in certain cases) leading to
	found being TRUE.
	*/

	__try
	{
		_asm
		{
			pushfd;						// save flag register
			or byte ptr[esp + 1], 1;	// set trap flag in EFlags
			popfd;						// restore flag register
		}
	}
	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		found = FALSE;
	}

	if (found)
	{

		exit(DBG_SINGLESTEPEXCEPTION);
	}
}

/*
* // adbg_Int3()
*
* // How it works:
* INT 3 is a standard software breakpoint (opcode 0xCC). When
* you set a breakpoint, your debugger replaces the first opcode
* under the breakpoint location with a 0xCC (INT 3). When the
* debugger hits this opcode it breaks and restores the original
* opcode. We add an exeption handler that switches 'found' from
* true to false. Without a debugger, *something must* handle the
* breakpoint exception (which is our handler). If our handler does
* not get hit, it means a debugger attempted to handle the
* exception itself, an in turn, leaving 'found' marked true.
*
* // Indication:
* Most debuggers go out of their way to hide the fact that they
* have replaced an opcode with 0xCC. In IDA for example, you need
* to specifically set an option to show these replacements. If you
* ever see an INT 3 instruction or a 0xCC (standalone) opcode,
* red flags should go up.
*
* // Bypass:
* Most debuggers will give you an option when an exception is
* thrown - either pass the exception to the application (and
* hope it's equipped to handle it), or discard the exception
* and have the debugger handle it instead. Your debugger is
* perfectly capacble of handling a breakpoint exception, but
* if your debugger handles this exception, 'found' is never
* marked false, and you're busted. When in doubt, pass
* exceptions to the application.
*/
void adbg_Int3(void)
{
	BOOL found = TRUE;

	__try
	{
		_asm
		{
			int 3;	// 0xCC standard software breakpoint
		}
	}

	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		found = FALSE;
	}

	if (found)
	{

		exit(DBG_INT3CC);
	}
}


/*
* // adbg_PrefixHop()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*
*/
void adbg_PrefixHop(void)
{
	BOOL found = TRUE;

	__try
	{
		_asm
		{
			__emit 0xF3;	// 0xF3 0x64 is the prefix 'REP'
			__emit 0x64;
			__emit 0xCC;	// this gets skipped over if being debugged
		}
	}

	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		found = FALSE;
	}

	if (found)
	{

		exit(DBG_PREFIXHOP);
	}
}


/*
* // adbg_Int2D()
*
* // How it works:
* ...
*
* // Indication:
* ...
*
* // Bypass:
* ...
*
*/
void adbg_Int2D(void)
{
	BOOL found = TRUE;

	__try
	{
		_asm
		{
			int 0x2D;	// kernel breakpoint
		}
	}

	__except (EXCEPTION_EXECUTE_HANDLER)
	{
		found = FALSE;
	}

	if (found)
	{

		exit(DBG_NONE);
	}
}








#pragma once
#ifndef OBFS_STRING_FUNC
#define OBFS_STRING_FUNC
#include <stdint.h>

//-------------------------------------------------------------//
// "Malware related compile-time hacks with C++11" by LeFF   //
// You can use this code however you like, I just don't really //
// give a shit, but if you feel some respect for me, please //
// don't cut off this comment when copy-pasting... ;-)       //
//-------------------------------------------------------------//

////////////////////////////////////////////////////////////////////
template <int X> struct EnsureCompileTime {
	enum : int {
		Value = X
	};
};
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
//Use Compile-Time as Myseed
#define Myseed ((__TIME__[7] - '0') * 1  + (__TIME__[6] - '0') * 10  + \
                  (__TIME__[4] - '0') * 60   + (__TIME__[3] - '0') * 600 + \
                  (__TIME__[1] - '0') * 3600 + (__TIME__[0] - '0') * 36000)
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
constexpr int LinearCongruentGenerator(int Rounds) {
	return 1013904223 + 1664525 * ((Rounds > 0) ? LinearCongruentGenerator(Rounds - 1) : Myseed & 0xFFFFFFFF);
}
#define Random() EnsureCompileTime<LinearCongruentGenerator(10)>::Value //10 Rounds
#define RandomNumber(Min, Max) (Min + (Random() % (Max - Min + 1)))
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
template <int... Pack> struct IndexList {};
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
template <typename IndexList, int Right> struct Append;
template <int... Left, int Right> struct Append<IndexList<Left...>, Right> {
	typedef IndexList<Left..., Right> Result;
};
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
template <int N> struct ConstructIndexList {
	typedef typename Append<typename ConstructIndexList<N - 1>::Result, N - 1>::Result Result;
};
template <> struct ConstructIndexList<0> {
	typedef IndexList<> Result;
};
////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////
const char XORKEY = static_cast<char>(RandomNumber(0, 0xFF));
__forceinline constexpr char EncryptCharacter(const char Character, int Index) {
	return Character ^ (XORKEY + Index);
}
template <typename IndexList> class CXorString;
template <int... Index> class CXorString<IndexList<Index...> > {
private:
	char Value[sizeof...(Index) + 1];
public:
	__forceinline constexpr CXorString(const char* const String)
		: Value{ EncryptCharacter(String[Index], Index)... } {}

	__forceinline char* decrypt() {
		for (int t = 0; t < sizeof...(Index); t++) {
			Value[t] = Value[t] ^ (XORKEY + t);
		}
		Value[sizeof...(Index)] = '\0';
		return Value;
	}

	__forceinline char* get() {
		return Value;
	}
};

const wchar_t XORKEYW = static_cast<wchar_t>(RandomNumber(0, 0xFFFF));
__forceinline constexpr wchar_t EncryptCharacterW(const wchar_t Character, int Index) {
	return Character ^ (XORKEYW + Index);
}
template <typename IndexList> class CXorStringW;
template <int... Index> class CXorStringW<IndexList<Index...> > {
private:
	wchar_t Value[sizeof...(Index) + 1];
public:
	__forceinline constexpr CXorStringW(const wchar_t* const String)
		: Value{ EncryptCharacterW(String[Index], Index)... } {}

	__forceinline wchar_t* decrypt() {
		for (int t = 0; t < sizeof...(Index); t++) {
			Value[t] = Value[t] ^ (XORKEYW + t);
		}
		Value[sizeof...(Index)] = '\0';
		return Value;
	}

	__forceinline wchar_t* get() {
		return Value;
	}
};

#define XorS(X, String) CXorString<ConstructIndexList<sizeof(String)-1>::Result> X(String)
#define XorString( String ) ( CXorString<ConstructIndexList<sizeof( String ) - 1>::Result>( String ).decrypt() )
#define XorSW(X, String) CXorStringW<ConstructIndexList<sizeof(String)-1>::Result> X(String)
#define XorStringW( String ) ( CXorStringW<ConstructIndexList<sizeof( String ) - 1>::Result>( String ).decrypt() )
////////////////////////////////////////////////////////////////////






#include <winnt.h>
#include <winternl.h>

constexpr uint32_t val_32_const = 0x811c9dc5;
constexpr uint32_t prime_32_const = 0x1000193;
constexpr uint64_t val_64_const = 0xcbf29ce484222325;
constexpr uint64_t prime_64_const = 0x100000001b3;

inline constexpr uint32_t hash_32_fnv1a_const(const char* const str, const uint32_t value = val_32_const) noexcept {
	return (str[0] == '\0') ? value : hash_32_fnv1a_const(&str[1], (value ^ uint32_t(str[0])) * prime_32_const);
}

inline constexpr uint64_t hash_64_fnv1a_const(const char* const str, const uint64_t value = val_64_const) noexcept {
	return (str[0] == '\0') ? value : hash_64_fnv1a_const(&str[1], (value ^ uint64_t(str[0])) * prime_64_const);
}


constexpr uint32_t cx_fnv_hash(const char* str) {
	return hash_32_fnv1a_const(str);
}

// Thread Environment Block (TEB)
#if defined(_M_X64) // x64
static PTEB tebPtr = reinterpret_cast<PTEB>(__readgsqword(reinterpret_cast<DWORD_PTR>(&static_cast<NT_TIB*>(nullptr)->Self)));
#else // x86
static PTEB tebPtr = reinterpret_cast<PTEB>(__readfsdword(reinterpret_cast<DWORD_PTR>(&static_cast<NT_TIB*>(nullptr)->Self)));
#endif
// Process Environment Block (PEB)

static void* GetModuleProcAddressByHash(void* moduleBase, uint32_t procNameHash) {

	PIMAGE_DOS_HEADER dosHeader = (PIMAGE_DOS_HEADER)moduleBase;
	if (dosHeader->e_magic != IMAGE_DOS_SIGNATURE) return NULL;
	PIMAGE_NT_HEADERS headers32 = (PIMAGE_NT_HEADERS)((char*)moduleBase + dosHeader->e_lfanew);
	if (headers32->Signature != IMAGE_NT_SIGNATURE) return NULL;
	if (headers32->FileHeader.SizeOfOptionalHeader < 96 || headers32->OptionalHeader.NumberOfRvaAndSizes == 0) return NULL;
	DWORD EdtOffset = headers32->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT].VirtualAddress;
	if (!EdtOffset) return NULL;

	typedef struct _EXPORT_DIRECTORY_TABLE {
		DWORD ExportFlags;
		DWORD TimeStamp;
		WORD MajorVersion;
		WORD MinorVersion;
		DWORD NameRVA;
		DWORD OrdinalBase;
		DWORD ExportAddressTableSize;
		DWORD NamePointerTableSize;
		DWORD ExportAddressTableRVA;
		DWORD NamePointerTableRVA;
		DWORD OrdinalTableRVA;
	} EXPORT_DIRECTORY_TABLE, * PEXPORT_DIRECTORY_TABLE;

	PEXPORT_DIRECTORY_TABLE EdtPtr =
		(PEXPORT_DIRECTORY_TABLE)((char*)moduleBase + EdtOffset);
	PVOID OrdinalTable = (PBYTE)moduleBase + EdtPtr->OrdinalTableRVA;
	PVOID NamePointerTable = (PBYTE)moduleBase + EdtPtr->NamePointerTableRVA;
	PVOID ExportAddressTable = (PBYTE)moduleBase + EdtPtr->ExportAddressTableRVA;

	for (DWORD i = 0; i < EdtPtr->NamePointerTableSize; i++) {
		DWORD NameRVA = ((PDWORD)NamePointerTable)[i];
		const char* NameAddr = (char*)moduleBase + NameRVA;

		//if (strcmp(NameAddr, procName))
		//	continue;
		if (cx_fnv_hash(NameAddr) != procNameHash)
			continue;


		WORD Ordinal = ((PWORD)OrdinalTable)[i] + (WORD)EdtPtr->OrdinalBase;
		WORD RealOrdinal = Ordinal - (WORD)EdtPtr->OrdinalBase;
		DWORD ExportAddress = 0;
		ExportAddress = ((PDWORD)ExportAddressTable)[RealOrdinal];
		void* FinalAddr = (char*)moduleBase + ExportAddress;
		return FinalAddr;
	}
	return NULL;
}
static void* GetProcPtr(uint32_t procNameHash, const wchar_t* dllName = NULL, const char* name = NULL) {
	//Get Pointer to PEB structure
	PPEB pebPtr = tebPtr->ProcessEnvironmentBlock;
	//Reference point / tail to compare against, since the list is circular
	PLIST_ENTRY moduleListTail = &pebPtr->Ldr->InMemoryOrderModuleList;
	PLIST_ENTRY moduleList = moduleListTail->Flink;
	//Traverse the list until moduleList gets back to moduleListTail
	do {
		char* modulePtrWithOffset = (char*)moduleList;
		//List is intrusive, a part of a larger LDR_DATA_TABLE structure,
		//so cast the pointer
		PLDR_DATA_TABLE_ENTRY module = (PLDR_DATA_TABLE_ENTRY)modulePtrWithOffset;
		//Compare the name of the entry against our parameter name
		//Note that the name is a wide string

		void* funcPtr = nullptr;
		//The actual position of the image base address inside
		//the LDR_DATA_TABLE_ENTRY seems to change *a lot*.
		//Apparently on Windows 8.1 it wasn't located in the
		//correct place according to my structures defined above.
		//It should have been "DllBase", but apparently it
		//was 8 bytes back, inside Reserved2[0]
		void* DllBase = module->Reserved2[0];

		if (!dllName || _wcsicmp(module->FullDllName.Buffer, dllName) == 0)
			if (funcPtr = GetModuleProcAddressByHash(DllBase, procNameHash)) {

#if defined(_DEBUG) || defined(_MY_DEBUG)
				if (name && strcmp(name, "DefWindowProcW1222_test") == 0) {

					wchar_t errMsg[1024] = { 0 };
					swprintf_s(errMsg, L"Module: %s\n%u\n", module->FullDllName.Buffer, (int)funcPtr);
					(MessageBoxW)(0, errMsg, (L"Find function HeapAlloc"), MB_OK);
				}
				else
#endif
					return funcPtr;
			}
		moduleList = moduleList->Flink;
	} while (moduleList != moduleListTail);
#if defined(_DEBUG) || defined(_MY_DEBUG)
	char errMsg[1024] = { 0 };
	sprintf_s(errMsg, "Function: %s\nHash: %u", name ? name : "NULL", procNameHash);
	(MessageBoxA)(0, errMsg, ("Can't find function"), MB_ICONERROR | MB_OK);
	ExitProcess(0);
#endif
	return NULL;
}




static void* get_func_by_hash(uint32_t hash, const wchar_t* dllName = NULL, const char* name = NULL) {
	return GetProcPtr(hash, dllName, name);
}
template <uint32_t hash>
static void* lazyimport_get(const wchar_t* dllName = NULL, const char* name = NULL)
{
	static void* pfn;
	if (!pfn)
		pfn = get_func_by_hash(hash, dllName, name);
	return pfn;
}

#if defined(_DEBUG) || defined(_MY_DEBUG)
#define IFN_DLL(dllName,name) (reinterpret_cast<decltype(&name)>(lazyimport_get<cx_fnv_hash(#name)>(dllName,#name)))
#define IFN(name) (reinterpret_cast<decltype(&name)>(lazyimport_get<cx_fnv_hash(#name)>(0,#name)))
#define IFN_PTR_DLL(dllName,name) (lazyimport_get<cx_fnv_hash(#name)>(dllName,#name))
#define IFN_PTR(name) (lazyimport_get<cx_fnv_hash(#name)>(0,#name))
#else
#define IFN_DLL(dllName,name) (reinterpret_cast<decltype(&name)>(lazyimport_get<cx_fnv_hash(#name)>(dllName)))
#define IFN(name) (reinterpret_cast<decltype(&name)>(lazyimport_get<cx_fnv_hash(#name)>()))
#define IFN_PTR_DLL(dllName,name) (lazyimport_get<cx_fnv_hash(#name)>(dllName))
#define IFN_PTR(name) (lazyimport_get<cx_fnv_hash(#name)>())
#endif // DEBUG


#endif