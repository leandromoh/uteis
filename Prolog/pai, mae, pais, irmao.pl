homem(paulo).
homem(lucas).

mulher(maria).
mulher(joana).

pai(paulo, lucas).
pai(paulo, joana).

mae(maria, lucas).
mae(maria, joana).

pais(X,Y) :- pai(X,Y).
pais(X,Y) :- mae(X,Y).

irmao(X,Y) :- pais(Z,X), pais(Z,Y), X \= Y.