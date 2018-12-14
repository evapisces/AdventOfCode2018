import plotly as plotly
import plotly.plotly as py
import plotly.graph_objs as go
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import re as re
plotly.tools.set_credentials_file(username='evapisces', api_key='fhcjQzGZHPDu7EVL5fAv')

import numpy as np

file = open("input.txt", "r")
lines = file.readlines()
positions = []
velocities = []

for line in lines:
  clean_line = line.strip("\n").replace(" ", "")
  position = re.search('position=<(.*)>velocity', clean_line)
  velocity = re.search('velocity=<(.*)>', clean_line)
  c = tuple(int(x) for x in position.group(1).split(','))
  positions.append(c)

  
  d = tuple(int(x) for x in velocity.group(1).split(','))
  velocities.append(d)

x_val = [i[0] for i in positions]
y_val = [i[1] for i in positions]

plt.scatter(x_val, y_val)
  
plt.show()
#trace = go.Scatter(
#  x = [i[0] for i in positions],
#  y = [i[1] for i in positions],
#  mode = 'markers'
#)

#data = [trace]

#py.plot(data, filename='day10-scatter0')

tempPositions = []

j = 1
for y in range(0, 100000):
  i = 0
  for p, v in zip(positions, velocities):
    t = (p[0]+v[0], p[1]+v[1])
    tempPositions.append(t)

  positions = tempPositions

  x_val = [i[0] for i in positions]
  y_val = [i[1] for i in positions]

  plt.scatter(x_val, y_val)
  
  

  #trace = go.Scatter(
  #  x = [i[0] for i in positions],
  #  y = [i[1] for i in positions],
  #  mode = 'markers'
  #)

  #data = [trace]

  #py.plot(data, filename='day10-scatter')
  tempPositions = []
  j = j + 1
plt.show()
  

  
