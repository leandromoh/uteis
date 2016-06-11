function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min);
}

function getRandomString(length){
  var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
  var lastIndex = chars.length - 1;
  var result = [];

  while(length--)
    result.push(chars[getRandomInt(0, lastIndex)]);

  return result.join('');
}

console.log(getRandomString(8));
