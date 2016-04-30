concat([],Ys,Ys).
concat([X|Xs],Ys,[X|Z]) :- concat(Xs,Ys,Z).

partition([],_,[],[]).
partition([X|Xs],Y,[X|Ls],Rs) :- X =< Y,partition(Xs,Y,Ls,Rs).
partition([X|Xs],Y,Ls,[X|Rs]) :- X > Y,partition(Xs,Y,Ls,Rs).

quicksort([],[]).
quicksort([X|Xs],Ys) :- partition(Xs,X,Left,Right),
                        quicksort(Left,Ls),
                        quicksort(Right,Rs),
                        append(Ls,[X|Rs],Ys).