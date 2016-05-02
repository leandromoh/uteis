% distribui(L,A,B) : distribui itens de L entre A e B
distribui([],[],[]).
distribui([X],[X],[]).
distribui([X,Y|Z],[X|A],[Y|B]) :- distribui(Z,A,B).

% intercala(A,B,L) : intercala A e B gerando L
intercala([],B,B).
intercala(A,[],A).
intercala([X|A],[Y|B],[X|C]) :- X =< Y, intercala(A,[Y|B],C).
intercala([X|A],[Y|B],[Y|C]) :- X > Y, intercala([X|A],B,C).

% ordena(L,S) : ordena a lista L gerando S
ordena([],[]).
ordena([X],[X]).
ordena([X,Y|Z],S) :- distribui([X,Y|Z],A,B),
                     ordena(A,As),
                     ordena(B,Bs),
                     intercala(As,Bs,S).
