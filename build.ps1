$csprojPathClient = ".\FivemToolsLib.Client\FivemToolsLib.Client.csproj"
$csprojPathServer = ".\FivemToolsLib.Server\FivemToolsLib.Server.csproj"

function build {
    Write-Output "Building Client library..."
    dotnet restore $csprojPathClient
    dotnet build $csprojPathClient -c Release --no-restore
    dotnet publish $csprojPathClient -c Release --no-build

    Write-Output "Building Server library..."
    dotnet restore $csprojPathServer
    dotnet build $csprojPathServer -c Release --no-restore
    dotnet publish $csprojPathServer -c Release --no-build
}

function docs {
    Write-Output "Building documentation pages..."
    docfx .\docfx.json --serve
}

function tag {
    Write-Output "Tagging project and pushing to git..."

    $version = Get-Content -Path "VERSION" -Raw
    $version = $version.Trim()

    if (-not $version) {
        Write-Error "Version file is empty or missing!"
        return
    }

    Write-Output "Current version from VERSION file: $version"
    Write-Host "Are you sure you want to tag this version? Type 'yes' to confirm: " -NoNewline
    $confirmation = Read-Host

    if ($confirmation -ne "yes") {
        Write-Warning "Tagging cancelled."
        return
    }

    $tagName = "fivemtoolslib/v$version"

    git tag $tagName

    git push origin $tagName

    Write-Output "Tagged as $tagName and pushed to origin."
}

function cleanup {
    Write-Output "Removing old/unused binaries, and folders..."

    $folders = @(
        ".\FivemToolsLib.Client\bin\",
        ".\FivemToolsLib.Client\obj\",
        ".\FivemToolsLib.Client\nupkg",
        ".\FivemToolsLib.Server\bin\",
        ".\FivemToolsLib.Server\obj\",
        ".\FivemToolsLib.Server\nupkg",
        ".\bin\",
        ".\obj\",
        ".\_site\",
        ".\api\"
    )

    foreach ($folder in $folders) {
        Remove-Item -LiteralPath $folder -Force -Recurse -ErrorAction SilentlyContinue
    }
}

if ($args.Count -eq 0) {
    Write-Output "No parameters provided. Usage:"
    Write-Output ".\build.ps1 [clean] [build] [docs] [tag]"
} else {
    foreach ($parameter in $args) {
        switch ($parameter) {
            "build" {
                build
            }
            
            "clean" {
                cleanup
            }

            "docs" {
                docs
            }

            "tag" {
                tag
            }

            Default {
                Write-Host "Unkown paramter: $($parameter)"
            }
        }
    }
}