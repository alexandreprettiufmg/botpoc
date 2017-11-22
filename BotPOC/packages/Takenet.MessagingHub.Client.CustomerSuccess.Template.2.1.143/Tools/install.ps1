param($installPath, $toolsPath, $package, $project)

$file1 = $project.ProjectItems.Item("application.json")
$file2 = $project.ProjectItems.Item("NLog.config")
$file3 = $project.ProjectItems.Item("mhh.exe.config")

# set 'Copy To Output Directory' to 'Copy always'
$copyToOutput1 = $file1.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 1

$copyToOutput2 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput2.Value = 1

$copyToOutput3 = $file3.Properties.Item("CopyToOutputDirectory")
$copyToOutput3.Value = 1