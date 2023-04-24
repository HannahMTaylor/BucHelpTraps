#!/bin/bash
# Created:        4/24/23
# Last updated:   4/24/23
# Author:          Daniel Dotson
# Project:         Software Engineering 2, BucHelpWeb app
# Description:     This script runs the FAQ API that Liam Whitelaw created.
#                  it binds the API to port 8080 on host. 
#                  It starts it in detached mode and restarts on reboot.
#					
# Version: 1.1
#

sudo docker run -d -p 8080:80 --restart unless-stopped lwhitelaw/buchelp-dbapi:latest