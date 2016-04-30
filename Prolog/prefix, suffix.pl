concat([],Y,Y).
concat([H|T],Y,[H|Z]) :- concat(T,Y,Z).

%prefix([1,2],[1,2,3]). -> True
prefix(Xs, Zs) :- concat(Xs,_,Zs).

suffix(Ys, Zs) :- concat(_,Ys,Zs).