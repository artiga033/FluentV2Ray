function ScanI18NKeys($xaml) { 
     $match = $xaml | Select-String -Pattern "ex:Locale.*?Key=(?<keyword>\w*)?}" -AllMatches
     $result = New-Object System.Collections.Generic.List[string]
     if ($match.Matches.Success) {
          $keywordMatches = $match.Matches.Groups | Where-Object { $_.Name -eq "keyword" } 
          foreach ($m in $keywordMatches) {
               $result.Add($m.Value)
          }
     }
     return $result
}

function UpdateResouceFile($filepath, $keywords) {
     [xml]$resw = Get-Content -Path $filepath -Raw
     $root = $resw.root
     $data = $root.data
     foreach ($d in $data) {
          if ($keywords.Contains($d.name)) {
               $keywords.Remove($d.name)
          }
     }
     foreach ($k in $keywords) {
          $node = $resw.CreateElement("data")
          $node.SetAttribute("name", $k)
          $node.SetAttribute("xml:space", "preserve")
          $valuenode = $resw.CreateElement("value")
          # $valuenode.InnerTex=""
          $node.AppendChild($valuenode)
          $root.AppendChild($node)
     }
     $resw.Save($filepath)
     write "Saved at" + $filepath
}

$dir = [System.IO.Path]::GetFullPath( [System.IO.Path]::Combine($PSScriptRoot, "..\..\"));
# scan files
$files = @(Get-ChildItem -Path $dir -File -Recurse | Where-Object { $_.Extension -eq ".xaml" } | Foreach-Object { $_.FullName });

$keys = New-Object System.Collections.Generic.HashSet[string]
foreach ($i in $files) {
     $c = Get-Content -Raw $i
     $results = ScanI18NKeys($c)
     foreach ($k in $results) {
          $keys.Add($k)
     }
}

Write-Output "Found keys: " + $keys.Count

$resfiles= Get-ChildItem ($dir + "Resources\Strings\") | Where-Object{$_.Extension -eq ".resw"} | Foreach-Object { $_.FullName }

foreach($i in $resfiles){
     UpdateResouceFile -filepath $i -keywords $keys
}