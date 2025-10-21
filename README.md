# 프로젝트 이름 Super I TEXT RPG


## 📖 목차
1. [프로젝트 소개](#프로젝트-소개)
2. [주요기능](#주요기능)
3. [개발기간](#개발기간)
4. [기술스택](#기술스택)
5. [와이어프레임](#와이어프레임)
6. [프로젝트 파일 구조](#프로젝트-파일-구조)
7. [Trouble Shooting](#trouble-shooting)


## 👨‍🏫 프로젝트 소개
- 프로젝트 명 : Super I TEXTRPG
- 프로젝트 설명 : ‘I’ 끼리 모여서 만든 텍스트알피지
- 프로젝트 시작 계기 : SPARTA 내일배움캠프_Unity 게임 개발 커리큘럼 참여 中 C# 코드를 사용한 팀 프로젝트 게임 제작 과제를 수행하기 위함
- 프로젝트 팀원
    - 팀장 : 김은서
    - 팀원 : 김하늘
    - 팀원 : 송덕희
    - 팀원 : 유원영
    - 팀원 : 장준혁
    - 팀원 : 전규태


## 💜 주요기능

- 기능 1
    - GameManager 에서 게임 실행의 전반적인 구동을 담당함.

- 기능 2
    - CharacterManager 에서 Player 캐릭터의 생성에 관한 역할을 담당함.

- 기능 3
    - JobManager 에서 Player 캐릭터의 직업군 생성 및 적용에 관한 역할을 담당함.

- 기능 4
    - BattleManager 에서 전투 흐름의 전반적인 구동을 담당함.

- 기능 5
    - SkillManager 에서 Player 캐릭터의 직업에 따른 Skill 적용에 관한 역할을 담당함.

- 기능 6
    - QuestManager 와 Quest 에서 Player 캐릭터의 Quest 현황을 처리하는 역할을 담당함.

- 기능 7
    - DataManager 에서 게임의 저장과 로드의 전반적인 구동을 담당함.


## ⏲️ 개발기간
- 총 7일   { 2025.10.14(화) ~ 2025.10.20(월) }


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
- **빌드 환경:** Unity 2022.3.17f1 (?)
- **배포 방식:** Windows .exe 파일 빌드
- **결과물:** `/Builds/Game.exe`


## 와이어프레임
<img width="2006" height="1238" alt="image" src="https://github.com/user-attachments/assets/4858b92b-8bab-4899-b783-f348a899d826" />


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
