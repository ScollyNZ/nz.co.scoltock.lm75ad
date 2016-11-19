# nz.co.scoltock.lm75ad

Searches the i2c bus on a Netduino to find all I2c devices and assumes anything that responds is an LM75AD.

Then polls each device for its current temp and prints to the debug window.

# To Do:
Doesn't currently support negative temps

# Notes:
LM75AD chips are available cheaply [here](https://www.aliexpress.com/item/10PCS-NEW-LM75AD-LM75A-LM75-SOP8-SOP-8-I2C-IIC-Digital-Temperature-Sensor-IC/32678279458.html?spm=2114.01010208.3.1.5mQC3e&ws_ab_test=searchweb0_0,searchweb201602_4_10065_10068_10084_10083_10080_10082_10081_10060_10061_10062_10056_10055_10037_10054_10033_10059_10032_10078_10079_10077_10073_10100_10096_10070_10052_423_10050_424_10051,searchweb201603_6&btsid=91a47f34-8cfb-40c6-bb43-dcdb57b0443e)
