```
                                                                           /$$
                                                                          |__/
 /$$   /$$  /$$$$$$  /$$$$$$/$$$$   /$$$$$$  /$$$$$$$$  /$$$$$$   /$$$$$$  /$$
| $$  | $$ /$$__  $$| $$_  $$_  $$ |____  $$|____ /$$/ |____  $$ /$$__  $$| $$
| $$  | $$| $$  \ $$| $$ \ $$ \ $$  /$$$$$$$   /$$$$/   /$$$$$$$| $$  \__/| $$
| $$  | $$| $$  | $$| $$ | $$ | $$ /$$__  $$  /$$__/   /$$__  $$| $$      | $$
|  $$$$$$$|  $$$$$$/| $$ | $$ | $$|  $$$$$$$ /$$$$$$$$|  $$$$$$$| $$      | $$
 \____  $$ \______/ |__/ |__/ |__/ \_______/|________/ \_______/|__/      |__/
 /$$  | $$                                                                    
|  $$$$$$/                                                                    
 \______/                                                                     
 
 ```

### Yomazari is a package manager for most distros. Specificilly made for Arch Linux and the distros that branch off of it.
 
### It is also made with intuitivity and user experience in mind.
 
# Installation

Installation is straightforward.

## 1. Install .NET Runtime

### Arch Linux and its derivatives
```bash
sudo pacman -S dotnet-runtime
````

### Debian / Ubuntu / Mint / Pop!_OS

```bash
sudo apt install dotnet-runtime-8.0
```

### Other distros

Install the .NET runtime using your package manager.

## 2. Clone the github repository

### Any distro
```bash
git clone https://www.github.com/NoSleepKid/yomazari
```
```bash
cd yomazari
```

### If you do not have [git](https://git-scm.com/) installed, install it.

### Arch Linux and its derivatives
```bash
sudo pacman -S git
```

### Debian / Ubuntu / Mint / Pop!_OS
```bash
sudo apt install git
```

## 2. Run the installer

### Any Distro
```bash
cd yoma-install
```
```bash
bash install.sh
```

This should prompt you with a friendly GUI that will guide you through the process.

Press the install button on the window that appears.

You will be prompted to enter your root password. This is because this installs GTK using your package manager.

The installer will take it on from there.
