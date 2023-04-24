#!/bin/bash
# Created:        4/19/23
# Last updated:   4/24/23
# Author:          Daniel Dotson
# Project:         Software Engineering 2, BucHelpWeb app
# Description:     This script requires the user to input the full repository location 
#                   and tag number for a docker image stored on docker hub. The script looks for a running instance at the specified port,
#                  shuts down that instance, and starts the new container with that port binding. 
#					It also checks for a running docker container at port 8080, which should correspond to the FAQ API.
# Version: 1.1
#
# TEST SCRPIT

# TODO: get port number from user here, check for valid number range and int. 



#enter the image location of the tag (i.e. drdotson14/buchelp:5.0)
read -rp "Enter the full address (i.e. drdotson14/buchelp:5.0) of the docker image (use lowercase): " imageLocation
echo ""

ipaddr=$(dig @ns1-1.akamaitech.net ANY whoami.akamai.net +short)

# TODO Error checking here

#Check to see if the API is running at port 8080:
if [[ -z $(docker ps | grep "0.0.0.0:8088->80") ]]
	then
		echo "No service is runing at port 8080; Your application may not connect to the FAQ API..."
		echo ""
	else
		echo "A service was found running on port 8080, hopefully it is your FAQ API..."
		echo ""
fi


#if there is no container running at port x, start up the new instance. x = 5000.
#if there is a container, shut down the running instance, and run the new image.
if [[ -z $(docker ps | grep "0.0.0.0:5000->80") ]];
  then
		echo ""
		echo "Staring $imageLocation at port 5000..."
		docker run -d -e APILOCATION=http://$ipaddr:8080 -p5000:80 --rm $imageLocation 
       
	else
		#cut the container ID from the docker ps command. 
		containerId=$(docker ps | grep "0.0.0.0:5000->80" | cut -f1 -d " ")
		echo ""
		echo "Stopping docker container: $containerId"
		
		 #stop the container.
		docker stop $containerId
		
		#run the container specified by the user.
		echo ""
		echo "Staring $imageLocation at port 5000..."
		docker run -p5000:80 -d --rm $imageLocation 
fi