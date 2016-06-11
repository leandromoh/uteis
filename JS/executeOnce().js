var sum = function(a,b){
  console.log(a + b);
}

var execFuncTimes = function(i,f){
  return function(){
    if(i-- > 0)
      f.apply(null,arguments);
  };
}

var execOnce = function(f){
  return execFuncTimes(1,f);
}

var sum1 = execOnce(sum);
sum1(3,1);
sum1(3,2);
sum1(3,3);


var exec1 = function(f){
  var i = 1;
  return function(){
    if(i-- > 0)
      f.apply(null,arguments);
  };
}

var sum2 = exec1(sum);
sum2(1,1);
sum2(2,2);
sum2(3,3);
