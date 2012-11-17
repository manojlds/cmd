using System.Diagnostics;

namespace cmd.Runner
{
    public class ProcessRunner : IRunner
    {
        public string Run(RunOptions runOptions)
        {
            var process = new Process
                        {
                            StartInfo =
                                {
                                    FileName = runOptions.Command,
                                    Arguments = runOptions.Arguments,
                                    UseShellExecute = false,
                                    RedirectStandardOutput = true
                                }
                        };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
    }
}
