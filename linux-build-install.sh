#!/bin/bash

TC_INSTALLPATH="${TC_INSTALLPATH:-/usr/local/bin}"
TC_USESUDO="${TC_USESUDO:-FALSE}"
dotnet publish -c Release -r linux-x64 --no-self-contained -o build
if [ "$TC_USESUDO" = "TRUE" ] ; then
sudo install -D -m755 build/tc-svg-merge "$TC_INSTALLPATH"
else
install -D -m755 build/tc-svg-merge "$TC_INSTALLPATH"
fi
