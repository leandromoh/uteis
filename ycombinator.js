// http://kestas.kuliukas.com/YCombinatorExplained/
// https://raganwald.com/2018/09/10/why-y.html

function ycomb(f){
  return f(f);
}

var fat = ycomb(f => n => n <= 1 ? 1 : f(f)(n-1) * n);

fat(5) // 120
