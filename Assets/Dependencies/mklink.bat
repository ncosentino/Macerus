mkdir bin
mkdir bin\ProjectXyz
mklink /D /J "bin\ProjectXyz\ProjectXyz.Application.Core" "..\..\..\ProjectXyz\ProjectXyz.Application.Core\bin\Debug"
mklink /D /J "bin\ProjectXyz\ProjectXyz.Data.Core" "..\..\..\ProjectXyz\ProjectXyz.Data.Core\bin\Debug"
mklink /D /J "bin\ProjectXyz\ProjectXyz.Data.Sql" "..\..\..\ProjectXyz\ProjectXyz.Data.Sql\bin\Debug"

mklink /D /J "bin\Tiled.Net" "..\..\..\Tiled.Net\Tiled.Net\bin\Debug"