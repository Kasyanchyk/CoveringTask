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
      if matrix[v][next_v] >= 1 and next_v != w[-2] and next_v not in w:
        path(w, next_v)
        w = w_copy[:]         
  # end.   
  
  
  
  # Anri-Laborder and Delmas algorithm
  def algorithm(x):
    result = [[] for i in range(len(x)//2)]
    
    # Multiplication
    for i in range(0, len(x) - 1, 2): # for every pair in boolean equation: ...
      for w in x[i]:
        if w in x[i+1]:
          result[i//2].append(w) # -> add x1*x1 == x1 etc  

      for w in x[i]:
        if w not in result[i//2]:
          for j in x[i+1]:
            if j not in result[i//2]:
              result[i//2].append(w + j) # -> add x2*x3 etc  
            
      for j in range(len(result[i//2])):
        result[i//2][j] = list(set(result[i//2][j])) # deleting an extra path that already exists in the element of equation 

    if len(x) % 2 != 0 and len(x) > 1:
      result.append(x[-1])
    if len(result) != 1:
      result = algorithm(result)
    return result
  # end.
        
    
    
  # Start:      
  ways = []
  for v in range(len(matrix)):
    if matrix[start][v] >= 1:            
      path([start], v)
  if len(ways) == 0: # -1 if there are no way from start to terminal vertex
    return -1
  
  
  # Boolean equation init
  be = [[] for i in matrix]
  for n in range(len(be)):
    for i in range(len(ways)):
      if n in ways[i]:
        be[n].append([str(i)])
  while [] in be:
    be.remove([])  
  be = algorithm(be)[0]
  

  # Forming resulting array (transforming array of path numbers -> array of paths with vertices)
  ans = [[] for i in be]
  for i in range(len(be)):
    for j in be[i]:
      ans[i].append(ways[int(j)])
  
  
  # Adding weight
  for ans_i in range(len(ans)):
    ans_w = 0
    for way_i in range(len(ans[ans_i])):
      for el_i in range(1, len(ans[ans_i][way_i])):
          ans_w += matrix[ans[ans_i][way_i][el_i - 1]][ans[ans_i][way_i][el_i]]
    ans[ans_i].append(ans_w)
  
  
  # Deleting non-optimal answers (by weight)
  optimum = ans[0][-1]
  for i in range(len(ans)):
    if ans[i][-1] < optimum:
      optimum = ans[i][-1]
  total_ans = []
  for i in range(len(ans)):
    if ans[i][-1] == optimum:
      total_ans.append(ans[i][:-1]) 

  return total_ans[0]