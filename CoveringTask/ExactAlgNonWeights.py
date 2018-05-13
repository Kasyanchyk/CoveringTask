def main_func(matrix, start, finish):
  def path(w, v):
    if v == finish:
        w.append(v)
        ways.append(w)
        return
    w.append(v)
    w_copy = w[:]
    for next_v in range(len(matrix)):
      if matrix[v][next_v] == 1 and next_v != w[-2] and next_v not in w:
          path(w, next_v)
          w = w_copy[:]

  ways = []
  for v in range(len(matrix)):
      if matrix[start][v] == 1:            
          path([start], v)
  x = set()
  for i in ways:
    x.update(i)
  x = len(x)

  from copy import copy

  def calc(z):
    g = set()
    for i in z:
      for j in ways[i]:
        g.add(j)
    g = list(g)
    if len(g) == x:
      if way_num[0] > len(z):
        way_num[0] = len(z)
        way_num[1] = z

  def combinations(target,data):
    for i in range(len(data)):
      new_target = copy(target)
      new_data = copy(data)
      new_target.append(data[i])
      new_data = data[i+1:]
      calc(new_target)
      combinations(new_target, new_data)
  target = []
  data = [i for i in range(len(ways))]
  way_num = [999999999, []]
  combinations(target, data)

  #print('Number of ways: ', way_num[0], '\nList of ways:')
  ans = []
  for i in way_num[1]:
    ans.append(ways[i])
  return ans