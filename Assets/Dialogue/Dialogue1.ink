-> premain

=== premain ===
Test of displaying all choices...
    * [Choice 1]
        -> main
    * [Bla-bla-bla]
        -> main
    * [Choice 3]
        -> main

=== main ===
Which pokemon do you choose?#KeyWord.pokemon #KeyWord.you #KeyWord.choose #Sin.0 #Sin.1
    * [Charmander] #Fact.0
        -> chosen("Charmander")
    * [Bulbasaur] #Fact.1
        -> chosen("Bulbasaur")
    * [Squirtle] #Fact.2
        -> chosen("Squirtle")
        
=== chosen(pokemon) ===
You chose {pokemon}! #Virtue.0 #Virtue.1
    -> ending()

=== ending ===
-> END