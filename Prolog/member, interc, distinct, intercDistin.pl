%define se um elemento é membro de uma lista
member([X|_],X).
member([_|T],X) :- member(T,X).

%intersecção retornando elementos repetidos: 
%interc([1,2,3,2],[1,2],Z). -> Z = [1,2,2]

interc([],_,[]).
interc([H|T],L,[H|Z]) :- member(L,H), interc(T,L,Z).
interc([_|T],L,Z) :- interc(T,L,Z).

%remove duplicidades nos elementos de uma lista: 
%distinct([1,2,2,3,2],Z). -> Z = [1,3,2]

distinct([],[]).
distinct([H|T],[H|Z]) :- not(member(T,H)), distinct(T,Z).
distinct([_|T],Z) :- distinct(T,Z).

%intersecção sem duplicidade nos elementos
intercDistin(X,Y,Z) :- interc(X,Y,W), distinct(W,Z).