$csprojPath = ".\FivemToolsLib.csproj"

function build {
    Write-Output "Building application..."
    dotnet restore $csprojPath
    dotnet build $csprojPath -c Release --no-restore
    dotnet publish $csprojPath -c Release --no-build
}

function docs {
    Write-Output "Building documentation pages..."
    docfx .\docfx.json
}

function tag {
    Write-Output "Tagging project and pushing to git..."

    $version = Get-Content -Path "VERSION" -Raw
    $version = $version.Trim()

    if (-not $version) {
        Write-Error "Version file is empty or missing!"
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
        ".\bin\",
        ".\obj\",
        ".\_site\",
        ".\api\",
        ".\nupkg"
    )

    foreach ($folder in $folders) {
        Remove-Item -LiteralPath $folder -Force -Recurse -ErrorAction SilentlyContinue
    }
}

if ($args.Count -eq 0) {
    Write-Output "No parameters provided. Usage:"
    Write-Output ".\build.ps1 [clean] [build] [docs]"
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