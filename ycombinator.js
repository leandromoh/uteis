function ycomb(f){
  return f(f);
}

var fat = ycomb(f => n => n <= 1 ? 1 : f(f)(n-1) * n);

fat(5) // 120
