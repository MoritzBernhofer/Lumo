using System.Diagnostics;
public class PythonExecutor {
    private string pythonPath;
    private string scriptPath;

    public PythonExecutor(string pythonPath, string scriptPath) {
        this.pythonPath = pythonPath;
        this.scriptPath = scriptPath;
    }

    public int Execute(int number) {
        // Create a new ProcessStartInfo object to specify the Python script and its arguments
        ProcessStartInfo startInfo = new ProcessStartInfo(pythonPath);
        startInfo.Arguments = $"{scriptPath} {number}";

        // Redirect the standard output to be able to read the output of the Python script
        startInfo.RedirectStandardOutput = true;

        // Create a new Process object and start it
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Read the output of the Python script
        string output = process.StandardOutput.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        return process.ExitCode;
    }
}