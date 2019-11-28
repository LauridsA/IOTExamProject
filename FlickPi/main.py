import paho.mqtt.client as mqtt
import flicklib as flicklib
import time
@flicklib.flick()
def flick(start, finish):
    global flickText
    flickText = start + '' + finish
@flicklib.tap()
def tap(position):
    global positionText
    positionText = '' + position
@flicklib.double_tap()
def doubletap(position):
    global doubletaptxt
    doubletaptxt = position
def setupMQTTClient():
    print('Setting up MQTT Client')
    global client
    client= mqtt.Client("FlickPi")
    client.username_pw_set("msxwryld","7z4Ms3G5-kfD")
    client.connect("farmer.cloudmqtt.com", 12393)
    print('Connected to MQTT broker')
    client.loop_start()
def postMQTTPrev():
    client.publish('track', 'prev')
def postMQTTNext():
    client.publish('track', 'next')
def postMQTTStart():
    client.publish('song', 'play')
def postMQTTStop():
    client.publish('song', 'stop')
def postMQTTPause():
    client.publish('song', 'pause')
def postMQTTUnPause():
    client.publish('song', 'unpause')
def main():
    print('We are running')
    setupMQTTClient()
    global positionText
    global flickText
    global doubletaptxt
    doubletaptxt = ''
    flickText = ''
    positionText = ''
    while True:
        if(flickText is 'eastwest' or positionText is 'west'):
            print('Enabled previous track')
            flickText = ''
            positionText  = ''
            postMQTTPrev()
        if(flickText is 'westeast' or positionText is 'east'):
            print('Enabled next track')
            flickText = ''
            positionText  = ''
            postMQTTNext()
        if(positionText is 'north'):
            print('enabled mqttplay with north press')
            positionText = ''
            postMQTTStart()
        if(positionText is 'south'):
            print('enabled mqttstop with south press')
            positionText = ''
            postMQTTStop()
        time.sleep(0.05)
main()