#!/bin/bash
# Created:        2/25/23
# Last updated:   3/2/23
# Author:          Daniel Dotson
# Project:         Software Engineering 2, BucHelpWeb app
# Description:     This script requires the user to input the full repository location 
#                   and tag number for a docker image stored on docker hub. The script looks for a running instance at the specified port,
#                  shuts down that instance, and starts the new container with that port binding. 
# Version: 1.0


## TODO: get port number from user here, check for valid number range and int. 

echo "RUNNING THIS SCRIPT WILL REPLACE THE PRODUCTION INSTANCE RUNNING AT PORT 80!"

#enter the image location of the tag (i.e. drdotson14/team3buchuntwebapp:firstpublish
read -rp "Enter the full address of the docker image: " imageLocation

# TODO Error checking here

#verify the image location.
echo $imageLocation


#if there is no container running at port x, start up the new instance. x = 3000.
#if there is a container, shut down the running instance, and run the new image.
if [[ -z $(docker ps | grep "0.0.0.0:80->80") ]];
  then
		docker run -p80:80-restart unless-stopped $imageLocation 
       
	else
		#cut the container ID from the docker ps command. 
		containerId=$(docker ps | grep "0.0.0.0:80->80" | cut -f1 -d " ")
		echo "Stopping docker container: $containerId"
		
		 #stop the container.
		docker stop $containerId
		
		#run the container specified by the user.
		docker run -p80:80 -d --restart unless-stopped $imageLocation 
fi


