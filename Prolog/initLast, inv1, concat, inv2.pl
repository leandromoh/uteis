%retorna o ultimo elemento de uma lista (last), e uma outra lista sem o ultimo elemento (init): 
%initLast([1,2,3],X,Y). -> X = [1, 2], Y = 3.

initLast([X],[],X).
initLast([H|T],[H|Z],Y) :- initLast(T,Z,Y).

%inverte a ordem dos elementos de uma lista
inv1([X],[X]).
inv1(L,[H|T]) :- initLast(L,I,H), inv1(I,T).

%concatena duas listas
%concat([1,2],[3],X). -> X = [1,2,3]

concat([],Y,Y).
concat([H|T],Y,[H|Z]) :- concat(T,Y,Z).

%inverte a ordem dos elementos de uma lista
inv2([X],[X]).
inv2([H|T],L) :- inv2(T,I), concat(I,[H],L).