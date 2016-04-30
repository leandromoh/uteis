%retorna uma lista com todos os valores entre o intervalo recebido
%range(1,5,X). -> X = [1, 2, 3, 4, 5]

range(L,L,[L]).
range(F,L,[F|Z]) :- X is F + 1, range(X,L,Z).

%retorna o quadrado de um numero
square(X,Y) :- Y is X^2.

%aplica todos os elementos de uma lista a um predicado e cria uma nova lista com os retornos
%map([2,3,4],square,X). -> [4, 9, 16]

map([],_,[]).
map([X|Xs],P,[Y|Ys]) :- call(P,X,Y), map(Xs,P,Ys).

%range(1,5,X), map(X,square,Y).