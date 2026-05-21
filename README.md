# Kiwiki's Great Escape 🐈

CSE 457 Final Artifact — Spring 2026

A stylized 3D puzzle/escape game built in Unity where the player controls a mischievous house cat trapped inside a futuristic smart apartment at night. Players explore the apartment, interact with objects, solve environmental puzzles, and escape using physics-based interactions.

Inspired by:

* Untitled Goose Game
* Little Kitty, Big City

---

# Team Members

* Hsu Wai Hnin Kyaw — Character Lead
* Larrianna Warner — Environment Lead
* Xuan Nhu Tran — Gameplay & Systems Lead
* Makalapua Goodness — Tech Art & Visuals Lead

---

# Unity Version

Unity 6000.3.13f1 LTS

(Other Unity 6000 LTS versions may work, but use the version above whenever possible.)

---

# Project Setup

## 1. Clone the Repository

```bash
git clone https://github.com/hsu01/kiwikis-great-escape.git
```

---

## 2. Install Git LFS

Git LFS is required for Unity asset files.

### Mac

Install Homebrew first if needed, then run:

```bash
brew install git-lfs
git lfs install
```

### Windows

1. Download Git LFS:
   https://git-lfs.com/

2. Install normally

3. Open Git Bash and run:

```bash
git lfs install
```

---

## 3. Open in Unity Hub

1. Open Unity Hub
2. Click "Add Existing Project"
3. Select the cloned project folder
4. Open the project using Unity 6000.3.1f1 LTS

---

# Important Unity Settings

In Unity:

Edit → Project Settings → Editor

Set:

* Version Control Mode = Visible Meta Files
* Asset Serialization = Force Text

These settings are required for proper Git collaboration.

---

# Team Workflow

## Before Starting Work

Always pull the newest changes first:

```bash
git pull
```

---

## After Finishing Work

Commit and push your changes:

```bash
git add .
git commit -m "Describe your changes"
git push
```

---

# Important Team Rules

* Do NOT edit the same scene simultaneously
* Use prefabs whenever possible
* Use separate test scenes for experimentation
* Put assets in the correct folders
* Commit frequently
* Push before ending your work session
* Pull before starting new work

---

# Recommended Folder Structure

Assets/

* Animations
* Audio
* Materials
* Models
* Prefabs
* Scenes
* Scripts
* Textures
* UI
* VFX
* ThirdParty

---

# Current MVP Goals

* Third-person cat movement
* Camera follow system
* Apartment graybox environment
* Physics-based object interaction
* One complete puzzle chain
* Escape ending

---

# Planned Features

## Gameplay

* WASD cat movement
* Jumping and climbing
* Puzzle interactions
* Physics-based collisions
* Object pushing / knocking

## Environment

* Stylized smart apartment
* Cozy nighttime lighting
* Interactive props
* Dynamic shadows

## Visual Effects

* Water particles
* Dust particles
* Breaking glass effects
* Emissive smart-device shaders
* Post-processing effects

## Animation

* Walk/run animations
* Idle animations
* Jumping
* Paw interaction animations
* Tail movement

---

# External Resources

Potential resources/tools:

* Unity Asset Store
* Mixamo
* Freesound
* Poly Haven
* AmbientCG
* Blender
* VS Code / Visual Studio

---

# Current Development Status

✅ Unity project setup

✅ GitHub + Git LFS setup

✅ Team folder structure

⬜ Cat controller

⬜ Graybox apartment

⬜ Puzzle system

⬜ Lighting polish

⬜ Final gameplay loop


---

# Notes

This project is being developed for the University of Washington CSE 457 Computer Graphics course.
