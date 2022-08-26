function ScanI18NKeysFromEnum($enumFile) {
     $content = Get-Content $enumFile
     $line = ($content | Select-String -Pattern "public enum").LineNumber
     if ($content[$line].Trim() -ne "{") {
          throw "Failed to Read $enumFile"
     }
     $keys = New-Object System.Collections.Generic.HashSet[string]
     while ($line += 1) {
          if ($content[$line].Trim() -eq "}") {
               break
          }
          [void]$keys.Add($content[$line].Trim("`t", ' ', ','))
     }
     return $keys
}

function UpdateResouceFile($filepath, $keywords) {
     [xml]$resw = Get-Content -Path $filepath -Raw
     $root = $resw.root
     $data = $root.data
     $added = [System.Linq.Enumerable]::Except([string[]]$keywords,[string[]]$data.Foreach("name"))

     foreach ($k in $added) {
          $node = $resw.CreateElement("data")
          $node.SetAttribute("name", $k)
          $node.SetAttribute("xml:space", "preserve")
          $valuenode = $resw.CreateElement("value")
          # $valuenode.InnerTex=""
          [void]$node.AppendChild($valuenode)
          $root.AppendChild($node)
     }
     $resw.Save($filepath)
     Write-Output "Saved at $filepath"
}

$dir = [System.IO.Path]::GetFullPath( [System.IO.Path]::Combine($PSScriptRoot, "..\..\"));

$enumFile = [System.IO.Path]::Combine($dir, "Extensions\Markup\LocaleKeys.cs")
$keys = ScanI18NKeysFromEnum($enumFile)

Write-Output "Found keys: " $keys.Count

$resfiles = Get-ChildItem ($dir + "Resources\Strings\") | Where-Object { $_.Extension -eq ".resw" } | Foreach-Object { $_.FullName }

foreach ($i in $resfiles) {
     UpdateResouceFile -filepath $i -keywords $keys
}