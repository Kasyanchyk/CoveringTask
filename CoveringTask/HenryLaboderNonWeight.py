def main_func(matrix, start, finish):
  
  
  
  # Finding set of paths
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

          
          
  # Anri-Laborder and Delmas algorithm
  def multiplication(x):
    result = [[] for i in range(len(x)//2)]
    for i in range(0, len(x) - 1, 2):
      for w in x[i]:
        if w in x[i+1]:
          result[i//2].append(w)
      for w in x[i]:
        if w not in result[i//2]:
          for j in x[i+1]:
            if j not in result[i//2]:
              result[i//2].append(w + j)            
      min_l = len(result[i//2][0])
      for j in result[i//2]:
        if len(j) < min_l:
          min_l = len(j)
      del_list = []
      for j in result[i//2]:
        if len(j) > min_l:
          del_list.append(j)
      for j in del_list:
        result[i//2].remove(j)

    if len(x) % 2 != 0 and len(x) > 1:
      result.append(x[-1])
    if len(result) != 1:
      result = multiplication(result)
    return result
  
  
  
  # Boolean equation init
  be = [[] for i in matrix]
  for n in range(len(be)):
    for i in range(len(ways)):
      if n in ways[i]:
        be[n].append([str(i)])
  while [] in be:
    be.remove([])
  be = multiplication(be)[0]
  
  # Forming resulting Array
  ans = [[] for i in be]
  for i in range(len(be)):
    for j in be[i]:
      ans[i].append(ways[int(j)])
  
  # Deletion non-optimal answers
  min_ans = len(ans[0])
  for i in ans:
    if len(i) < min_ans:
      min_ans = len(i)
  del_list = []
  for i in range(len(ans)):
    if len(ans[i]) > min_ans:
      del_list.append(i)
  for i in del_list:
    del ans[i]
  return ans[0]