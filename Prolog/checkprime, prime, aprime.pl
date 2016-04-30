checkprime(N,X) :- X mod N =\= 0, checkprime(N+1,X).
checkprime(N,X) :- X =:= N.

:- dynamic prime/1.
prime(1).
prime(X) :- checkprime(2,X).

aprime(X) :- (prime(X) -> asserta(prime(X)) ; asserta(notprime(X))).