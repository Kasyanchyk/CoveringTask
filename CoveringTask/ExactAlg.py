def main_func(matrix, start, finish):
  def path(w, v):
    if v == finish:
        w.append(v)
        ways.append(w)
        return
    w.append(v)
    w_copy = w[:]
    for next_v in range(len(matrix)):
      if matrix[v][next_v] >= 1 and next_v != w[-2] and next_v not in w:
          path(w, next_v)
          w = w_copy[:]

  ways = []
  for v in range(len(matrix)):
      if matrix[start][v] >= 1:            
          path([start], v)
  x = set()
  for i in ways:
    x.update(i)
  x = len(x)
  for i in range(len(ways)):
    w = 0
    for j in range(1, len(ways[i])):
      w += matrix[ways[i][j-1]][ways[i][j]]
    ways[i].append(w)
  from copy import copy

  def calc(z):
    g = [[], 0]
    for i in z:
      g[1] += ways[i][-1]
      for j in ways[i][:-1]:
        g[0].append(j)
    g[0] = list(set(g[0]))
    if len(g[0]) == x:
      if way_num[2] > g[1]:
        way_num[0] = len(z)
        way_num[1] = z
        way_num[2] = g[1]

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
  way_num = [999999999, [], 999999999]
  combinations(target, data)

  #print('Number of ways: ', way_num[0], '\nWeight of ways: ', way_num[2], '\nList of ways:')
  ans = []
  for i in way_num[1]:
    ans.append(ways[i][:-1])
  return ans