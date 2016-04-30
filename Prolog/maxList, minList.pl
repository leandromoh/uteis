%retorna o maior valor de uma lista
maxList([X],X).
maxList([H|T],H):- maxList(T,N), H >= N.
maxList([H|T],N):- maxList(T,N), N >  H.


minList([X],X).
minList([H|T],H):- minList(T,N), H =< N.
minList([H|T],N):- minList(T,N), N <  H.