# SUPER SIMPLE ORM
Just a toy, not for srsbsns.

# CODE

## The Attributes

### Table
The Table attribute is used at the class level to mark a class as being an entity. Add it to a class and specify the schema and table name.

### Column
The Column attribute is used at the Property level. I think I also made it available for fields, but didn't add any code
to handle that so probably don't use that. It has two properties:

#### Name
This is the name of the column in the data base. If the property in the class has the same name as the column in the database, this field can be left blank.

#### IsPk
Boolean that indicates the column is the primary key for the table. Please don't define two or more, there's no code to handle that. Please don't define 0, it'll break.

## The Doers of Things
There's only one doer of things, really, the SqliteConnector. It implements GetById, which has to be manually defined. Lame. Oh and the DB has to be copied to the output directory. Fancy.

## Putting it all together
So, create a class (in this case, FooEntity and give the class a Table attribute. Then give some properties some Column attributes.
In the main method, create an instance of the SqliteConnector, do a GetById\<FooTable\>(1) and you'll get back a FooTable object populated with values from the DB!

But, like, don't try to do anything else. If you pass in ID 7, for instance, it'll break. It's very fragile.
