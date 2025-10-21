# 프로젝트 이름 Super I TEXT RPG

## 📖 목차
1. [프로젝트 소개](#프로젝트-소개)
2. [주요기능](#주요기능)
3. [개발기간](#개발기간)
4. [기술스택](#기술스택)
5. [서비스 구조](#서비스-구조)
6. [와이어프레임](#와이어프레임)
7. [API 명세서](#API-명세서)
8. [ERD](#ERD)
9. [프로젝트 파일 구조](#프로젝트-파일-구조)
10. [Trouble Shooting](#trouble-shooting)

## 👨‍🏫 프로젝트 소개
## 프로젝트 계기
## 💜 주요기능

- 기능 1

- 기능 2

- 기능 3

- 기능 4


## ⏲️ 개발기간
- 2025.10.14(화) ~ 2025.10.20(월)
## 📚️ 기술스택
### 🖥️ Language
*  C#
### 🔧 Version Control
*  Git + GitHub
### 🧩 IDE
* Visual Studio
### 🧰 Framework
* net9.0
### 🚀 배포 (Deploy)
- **빌드 환경:** Unity 2022.3.10f1
- **배포 방식:** Windows .exe 파일 빌드
- **결과물:** `/Builds/Game.exe`
### 💾  DBMS

## 서비스 구조

## 와이어프레임

## ERD

## 프로젝트 파일 구조
C:.
|   .gitattributes
|   .gitignore
|   TEAMPROJECT_TEXTRPG.sln
|
\---TEAMPROJECT_TEXTRPG
    |   Program.cs
    |   TEAMPROJECT_TEXTRPG.csproj
    |
    \---Bin
        \---Debug
            +---Net9.0
            |       Newtonsoft.Json.dll
            |       TEAMPROJECT_TEXTRPG.deps.json
            |       TEAMPROJECT_TEXTRPG.dll
            |       TEAMPROJECT_TEXTRPG.exe
            |       TEAMPROJECT_TEXTRPG.pdn
            |       TEAMPROJECT_TEXTRPG.runtimeconfig.json
    |
    +---Core
    |       Item.cs
    |       Job.cs
    |       Monster.cs
    |       Player.cs
    |       Quest.cs
    |       Skill.cs
    |
    +---Managers
    |       BattleManager.cs
    |       CharacterManager.cs
    |       DataManager.cs    
    |       GameManager.cs
    |       JobManager.cs
    |       QuestManager.cs
    |       SkillManager.cs
    |
    \---obj
    |       project.asstes.json
    |       project.nuget.json
    |       TEAMPROJECT_TEXTRPG.csproj.nuget.dgspec.json
    |       TEAMPROJECT_TEXTRPG.csproj.nuget.g.props
    |       TEAMPROJECT_TEXTRPG.csproj.nuget.g.targets
        \---Debug
            \---Net9.0
            |       .NETCoreApp,Version=v9.0.AssemblyAttributes.cs
            |       apphost.exe
            |       TEAMPROJ.A7AC528E.Up2Date
            |       TEAMPROJECT_TEXTRPG.AssemblyInfo.cs
            |       TEAMPROJECT_TEXTRPG.AssemblyInfoInputs.cache
            |       TEAMPROJECT_TEXTRPG.assets.cache
            |       TEAMPROJECT_TEXTRPG.csproj.AssemblyReference.cache
            |       TEAMPROJECT_TEXTRPG.csproj.BuildWithSkipAnalyzers
            |       TEAMPROJECT_TEXTRPG.csproj.CoreCompileInputs.cache
            |       TEAMPROJECT_TEXTRPG.csproj.FileListAbsolute.txt
            |       TEAMPROJECT_TEXTRPG.dll
            |       TEAMPROJECT_TEXTRPG.GeneratedMSBuildEditorConfig.editorconfig
            |       TEAMPROJECT_TEXTRPG.genruntimeconfig.cache
            |       TEAMPROJECT_TEXTRPG.GlobalUsing.g.cs
            |       TEAMPROJECT_TEXTRPG.pdb
            |       TEAMPROJECT_TEXTRPG.sourcelink.json
            |
                +---ref
                |       TEAMPROJECT_TEXTRPG.dll
                |
                +---refint
                |       TEAMPROJECT_TEXTRPG.dll
    |
    +---SaveData
    |       PlayerData1.json
    |
    +---Scenes
    |       HomeScene.cs
    |       InitCharacterScene.cs
    |       IntroScene.cs
    |       LoadHomeScene.cs
    |       LoadScene.cs
    |       NewBattleResultScene.cs
    |       PlayerInfoScene.cs
    |       QuestScene.cs
    |       SaveScene.cs
    |       Scene.cs
    |       SelectJobScene.cs
    |       TotalBattleScene.cs
    |
    +---Utility
    |       ConsoleUtility.cs
    |       InputHandler.cs
    |       UIColorUtility.cs
    |
    \---_Lagacy
        +---Scenes
        |       BattleResultScene.cs
        |       BattleScene.cs
        |       BattleStartScene.cs
        |       EnemyTurnScene.cs
        |
        +---Skills
                SkillAction.cs
    |
    +---_Unfinished
## Trouble Shooting
