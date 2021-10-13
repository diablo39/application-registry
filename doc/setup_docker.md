# How to setup docker on linux without docker for windows and wsl

1. Install windows features: https://stackoverflow.com/questions/18683092/how-to-run-ssh-add-on-windows
2. Generate ssh keys
3. Restart pc
4. Install virtual machines with ubuntu on VirtualBox
   1. add hostonly network
      1. Disable dhcp
      2. https://askubuntu.com/questions/293816/in-virtualbox-how-do-i-set-up-host-only-virtual-machines-that-can-access-the-in
      3. https://askubuntu.com/questions/293816/in-virtualbox-how-do-i-set-up-host-only-virtual-machines-that-can-access-the-in/1013467#1013467
   2. add set natnetwork (use netplan)
      1. (ip -c link) - list of network interfaces
      2. enable dhcp for interface
5. https://www.chrisjhart.com/Windows-10-ssh-copy-id/ - log in onto linux without password
6. Value in hosts file - set friendly name for docker host ex.: 192.168.56.12 dockerlocal
7. set in /etc/hosts file value 192.168.56.1 host.docker.internal