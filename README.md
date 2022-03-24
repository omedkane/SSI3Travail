# Sujet Spécial Informatique III

## Travail I : Processus de Test

### Étape 1: Tests Unitaires, Écriture manuel vs génération.

#### Terminologie :

* **Stub** : Surcharge les méthodes pour retourner des données codées en dur (fixes)
  
  > **Example** : Considérons une méthode `Calculer()` qui prend 5 minutes pour compléter, au lieu de patienter cette durée lors du test, nous pourrions remplacer l'implémentation avec un **stub** qui retournerai des données fixes en une fraction de temps.

* **Mock** : Un peu similaire à un **stub** mais donne une grande importance au comportement, le mock peut prendre en compte l'ordre des appels des méthodes, les arguments passés à ces dernières et aussi le nombre d'appels effectués.
  
  > Example : Nous testons la classe `EnregistrementUtilisateur`, après avoir appelé la méthode `Sauvegarder()`, cette dernière devrait appeler `EnvoyerEmailDeConfirmation()`

#### Cas 1: Classe Instanciable

Pour cette étape, nous allons développer une classe qui encapsulera un nomber entier et effectuera des opérations sur ce même nombre, cela dit chacune des méthodes de cette classe devra retourner l'objet lui-même, cela nous permettra d'enchaîner les opérations et éviter un code verbeux.

La classe sera nommée `IntChain` et implémentera l'interface `IIntChain`:

```csharp
public interface IIntChain {

    // Retourne le nombre
    public int Get();

    // Ajoute "num" au nombre
    public IntChain Add(int num);

    // Soustrait "num" au nombre
    public IntChain Sub(int num);

    // Multiplie le nombre par "num";
    public IntChain Times(int num);

    // Divise le nombre par "num"
    public IntChain DivBy(int num);

    // Incrémente le nombre
    public IntChain Increment();

    // Décremente le nombre
    public IntChain Decrement();

}
```

Voir [IntChain.cs](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulator/IIntChain.cs)

##### 1.1 Test Unitaire Manuel

Pour conduire nos tests, nous utiliserons MSTest, le framework de tests unitaires qui se livre avec l'EDI de Visual Studio.

Pour procéder, nous avons créé un fichier titré [IntChainWrittenTest.cs](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/IntChainWrittenTest.cs) contenant la classe `IntChainWrittenTest` qui encapsule des méthodes homologues à chacunes de la classe à tester `IntChain`. N'ayant aucune interaction avec une autre classe, le test de la classe `IntChain` est relativement simple et ne requiert aucun mock ni stub.

Le sujet de nos tests sera un membre de type `IntChain` qu'on nommera `Chain` qu'on initialisera avec la valeur encapsulée de **20**.

```csharp
IntChain Chain = new(20);
```

Voici le test pour la méthode `Add()`, cette dernière prend un nombre entier comme argument et l'additionne à la valeur principale (la valeur encapsulée).

```csharp
[TestMethod]
public void Add() {
    // Ajoute 10 à la valeur principale
    Chain.Add(10);
    // Asserte que la valeur principale est égale à 30 la valeur attendue
    Assert.AreEqual(
        Chain.Get(),
        30 // (10 + 20)
    );
}
```

[Voir le code complet](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/IntChainWrittenTest.cs)

Comme le spécifie les commentaires, nous additionons **10** à la valeur encapsulée avec la méthode testée `Add()` puis nous assertons que la nouvelle valeur est égale à **30** la valeur attendue.

Et nous avons aussi développé la méthode `ChainedOperationsTest` pour tester l'objectif principal de notre classe, la possibilité d'enchaîner des opérations pour réduire la verbosité de notre code.

```csharp
[TestMethod]
public void ChainedOperationsTest() {
    Chain.Add(10).Sub(10).Times(2).DivBy(1).Increment().Decrement();

    Assert.AreEqual(
        Chain.Get(),
        40 // 20 + 10 - 10 * 2 / 1 + 1 - 1 = 40
    );
}
```

##### 1.2 Test Unitaire Généré

Nous allons générer les tests unitaires pour notre classe `IntChain` avec l'extension **Unit Test Boilerplate Generator (UTBG)** disponible et téléchargeable dans le marketplace de **Visual Studio**. La génération s'effectue sous quelques étapes notamment un clic droit sur la classe de votre choix et un clic gauche sur **Create Unit Test Boilerplate** qui par défaut générera le code nécessaire dans votre répertoire de tests suivant le chemin équivalent.

N'étant pas une surprise, le code généré ne teste pas magiquement les méthodes de la classe en devinant l'objectif de chaque fonction, plutôt le générateur analyse la structure de la classe, ses méthodes et les arguments de ces dernières pour nous fournir un model de code adapté à `IntChain`, en d'autres termes un **boilerplate**.

**UTBG** travaille conjointement avec un **framework de mocking** utilisé pour créer des objets substitutifs, et il supporte une variété de frameworks incluant le framework le plus populaire, **Moq**.

La classe de tests unitaires générée est structurée de la suivante sorte :

- Un membre de type `MockRepository`, une classe répertoire utilitaire pour fabriquer plusieurs **mocks** lorsqu'une vérification cohérente est souhaitée pour la totalité de ces derniers ;
  
  ```csharp
  private MockRepository mockRepository;
  ```
* Une méthode annotée par l'attribut `TestInitialize`, cette méthode est exécutée avant chaque test, dans cette méthode le générateur initialize `mockRepository`;
  
  ```csharp
  [TestInitialize]
  public void TestInitialize() {
      this.mockRepository = new MockRepository(MockBehavior.Strict);
  }
  ```

* À l'opposé de notre manière de procéder, au lieu d'initialiser notre sujet de test `IntChain` en tant que membre de la classe, le générateur préfère créer une méthode usine retournant une nouvelle instance de `IntChain`. Un appel à cette méthode sera fait dans chaque test, sans doute pour s'assurer d'une nouvelle instanciation, quoique dans la version actuelle de **MSTest**, les membres sont réinitialisés pour chaque test.
  
  ```csharp
  private IntChain CreateIntChain() => new IntChain();
  ```

* Et enfin pour chaque méthode de la classe `IntChain`, la méthode testant cette dernière. Le nom de la méthode testante se compose du nom de la méthode testée suffixé par **StateUnderTest_Expected_Behavior**.
  
  ```csharp
  [TestMethod]
  public void Add_StateUnderTest_ExpectedBehavior() {
      // Arrange
      var intChain = this.CreateIntChain();
      int num = 0;
  
      // Act
      var result = intChain.Add(num);
  
      // Assert
      Assert.Fail();
      this.mockRepository.VerifyAll();
  }
  ```
  
  Comme mentioné précedemment, les méthodes testantes font appel à `CreateIntChain()` pour obtenir une nouvelle instance de la classe testée et cela se verifie dans le code ci-dessus avec la variable `intChain`. Le générateur détecte sans problème le type de l'argument à passer dans la méthode testée et déclare une variable du même nom que le paramètre (**num**) sous la section **Arrange**  et lui donne la valeur **0**. Mais il ne présume faire aucune supposition au niveau de l'assertion et donc signale un échec d'assertion et laisse au bon vouloir du testeur d'apporter les modifications convenables pour mener à bien son test unitaire.

#### Cas 2 : Classe Statique

N'étant pas totalement satisfait des classes `Object` et `Reflection` de C#, nous avons décidé de développer une classe que l'on a nommé `ObjectHelper` pour effectuer des modifications communes aux objets quelque soit la classe qu'ils instancient, il s'agit là d'un avantage qu'offre les languages tels que **Javascript** et **Typescript**. `ObjectHelper` nous permettra de modifier les propriétés d'un objet de classe et aussi de les copier et les transposer vers un autre objet qui, bien evidemment, devra présenter des propriétés communes à celles du premier.

La classe statique `ObjectHelper` est structurée de la suivante sorte :

```csharp
public static class ObjectHelper 
{
    public static IEnumerable<string> GetCommonProperties<TSource, TTarget>(
        Type? typeOfSource = null,
        Type? typeOfTarget = null
    ) { };

    public static TTarget CopyTo<TSource, TTarget>(
        TSource source,
        TTarget target,
        Type? typeOfSource = null,
        Type? typeOfTarget = null,
        IEnumerable<string>? commonProperties = null
    ) { }

    public static void Set<T>(T target, string property, object value) { }
} 
```

##### 2.1 Test Unitaire Manuel

Nous avons suivi la même procédure que celle avec la classe instanciable à une seule exception près, nous n'avons instancié aucun sujet de classe car les classes statiques ne peuvent être instanciées car elles n'encapsulent que de membres statiques.

La méthode `Set()` permet définir la valeur d'une propriété d'un objet de classe, elle prend comme argument l'objet en question, le nom de la propriété et la valeur.

Voici la méthode testante de `Set()`, voir l'intégralité du code du test [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/Miscellaneous/Classes/ObjectHelperTest.cs)

```csharp
[TestMethod]
public void Set() {
    // Arrange
    DummyUser dummyUser = new DummyUser();
    string newName = "Michael";

    // Act
    ObjectHelper.Set(dummyUser, "FirstName", newName);

    // Assert

    // Vérifie que la propriété a été définie avec succés en comparant
    // sa valeur avec l'originale
    Assert.AreEqual(dummyUser.FirstName, newName);

    // Vérifie que notre exception personnalisée ValueTypeMismatchException
    // est levée quand les types des valeurs ne correspondent pas.
    Assert.ThrowsException<ValueTypeMismatchException>(
        () => ObjectHelper.Set(dummyUser, "FirstName", 987456)
    );

    // Vérifie que notre exception personnalisée InexistantPropertyException
    // est levée quand la propriété passée comme argument est inexistante.
    Assert.ThrowsException<InexistantPropertyException>(
        () => ObjectHelper.Set(dummyUser, "JeNexistePas", "PasDuTout")
    );

}
```

##### 2.2 Test Unitaire Généré

En effectuant une génération de test unitaire avec **Unit Test Boilerplate Generator**, nous avons remarqué qu'il ne faisait aucune différence entre une classe instanciable et une classe statique; Cela se prouve par sa création d'une méthode usine `CreateObjectHelper()` qui tente de retourner une instance de la classe `ObjectHelper` alors que cette dernière ne peut être instanciée car, je rappelle, elle est statique. Ajouté à cela, il n'a aucune notion des méthodes génériques, par exemple, il essaie d'instancier un objet de Type `T` alors que `T` est, d'un premier lieu, un paramétre de type générique et, d'un second lieu, il est directemment déduit (inferrence) du premier argument de la méthode, `target`.

Voici la méthode testante de `Set()`, voir l'intégralité du code du test [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/Miscellaneous/Classes/ObjectHelperTest.cs)

```csharp
[TestMethod]
public void Set_StateUnderTest_ExpectedBehavior() {
    // Arrange
    var objectHelper = this.CreateObjectHelper(); // Erreur !
    T target = default(T); // Erreur !
    string property = null;
    object value = null;

    // Act
    objectHelper.Set(
        target,
        property,
        value
    );

    // Assert
    Assert.Fail();
    this.mockRepository.VerifyAll();
} 
```

#### Conclusion

Le coût de la correction d'une erreur croît avec l'avancement dans le processus de développement. Le coût de la correction d'une erreur manifestée lors des tests de validations peut être **20 à 50 fois** plus importante que si découverte à l'étape des spécifications et **100 fois**, après la mise en service. Les **tests unitaires** répondent à ce dilemme; Lorsqu'ils sont écrits lors du stage de développement, plusieurs bugs sont détéctés et sont traités avant que ces derniers ne glissent dans les stages suivants.

Cela dit, développer les tests unitaires prend un long temps et piège le développeur dans un cycle d'engagements et de désengagements, et c'est pour cela que **la génération de tests** vient à la rescousse. Avoir un **boilerplate personnalisé** pour la classe à tester allége considérablement la charge de travail du développeur et lui épargne une bonne quantité de temps, ce qui est un but majeur de l'amélioration des processus.

Il importe de mentionner que le générateur génère pour toutes les méthodes de la classe à tester, alors que ce n'est pas nécessaire pour certaines méthodes qui sont plutôt simples et directes; Cependant cette remarque est sujet à une différence d'opinions sur la couverture des tests unitaires, tandis que certaines opinions promeuvent la **couverture totale** (que toutes les méthodes soient testées), d'autres estiment que cette pratique est redondante et que la présence d'un excès de tests est signe d'une augmentation de **complexité cyclomatique**.

### Étape 2 : Test d'un programme concret

Pour cette étape nous avons décidé de développer une **pâle** simulation d'un système de gestion de base de données relationnelles (SGBD).

Comme tout bon SGBD, celui là se compose de **bases de données** qui à leur tour se compose de **tables** et qui à leur tour se composent de **tuples** (lignes).

Ci-dessous se lit l'interface `IRow` qui sera implementée par la classe `Row` qui représente les **tuples** (lignes) :

```csharp
public interface IRow<Model> where Model : TableModel {
    public Guid ID { get; }
    public Row<Model>? Update(string property, object value);
    public Row<Model> Update(Action<Model> updator);
}
```

Voir le code complet de `Row` [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulator/Database/Row.cs) et celui de `RowTest` [là](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/Database/RowTest.cs)

Tous les tuples d'une table suivent un seul modèle et ce dernier doit étendre la classe abstraite `TableModel` et implémenter ses méthodes, une classe qu'on a préféré garder simple en suivant **une seule** règle celle d'avoir une seule **clé primaire** qui ne pourrait être changé après avoir été définie.

`Row` fait usage de la classe `ObjectHelper` pour modifier la valeur d'un attribut en **runtime** (en pleine exécution du programme).

Voici la classe abstraite `TableModel` :

```csharp
public abstract class TableModel {
    private Guid _ID;
    public Guid ID {
        get => _ID;
        set {
            if (_ID.Equals(Guid.Empty) {
                _ID = value;
            }
            else
                throw new PrimaryKeyException();
        }
    }

    public TableModel() { }

    public TableModel(Guid id) {
        ID = id;
    }
}
```

La classe `Table` implémente l'interface `ITable` :

```csharp
public interface ITable<Model> where Model : TableModel {

    public void Insert(Model record);

    public void InsertAll(IEnumerable<Model> records);

    public Row<Model> Find(Guid id);

    public QueryResult<Row<Model>, Guid> FindAll(IEnumerable<Guid> listOfIDs);

    public List<Row<Model>> FindWhere(Func<Model, bool> where, int? limit);

    public Row<Model> FirstWhere(Func<Model, bool> where);

    public MutationResult<Guid, Guid> UpdateAll(IEnumerable<Guid> listOfIDs, Action<Model> updator);

    public List<Guid> UpdateWhere(Func<Model, bool> where, Action<Model> updator);

    public void Remove(Guid id);

    public List<Guid> RemoveWhere(Func<Model, bool> where);

    public List<Row<Model>> GetAll();

}
```

Voir le code complet de `Table` [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulator/Database/Table.cs) et celui de `TableTest` [là](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/Database/TableTest.cs)

Notablement, elle présente les méthodes permettant de lire, d'insérer, de mettre à jour un ou plusieurs tuples (soit par leurs **ID**s ou soit par un prédicat aux conditions ils doivent correpondre) et de supprimer ses tuples.

Et enfin la classe `Database` implémente `IDatabase` qui se structure de la suivante sorte :

```csharp
public interface IDatabase {

    Table<T> CreateTable<T>(T model) where T : TableModel;

    Table<T> GetTable<T>() where T : TableModel;

    void Remove<T>() where T : TableModel;

    Table<NewModel> Update<OldModel, NewModel>()
        where OldModel : TableModel
        where NewModel : TableModel, new();
}
```

 Voir le code complet de `Database` [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulator/Database/Database.cs) et celui de `DatabaseTest` [là](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/Database/DatabaseTest.cs).

À son tour, `Database` aussi offre les fonctions **CRUD** (Création, Lecture, Mise-à-jour et Suppression) pour ses tables.

Comme mis en évidence textuellement, l'**ordre d'intégration** est la suivante :

```
Database {
    Table<TableModel> {
        Row<TableModel> {
            ObjectHelper
        }
    }
}
```

Pour conduire un test cohésif d'un système, il est fondamental de commencer de la classe la plus indépendante à la plus dépendante. Dans notre cas, la classe `ObjectHelper` est la plus indépendante car elle ne fait appel à aucune méthode des autres classes et n'accéde à aucun membre de ces dernières, puis comme illustré dans le schéma ci-dessus `Row`, `Table` et finalement `Database`.

Voir l'intégralité du test [ici](https://github.com/omedkane/SSI3Travail/blob/master/DatabaseSimulatorTest/).

### Étape 3 : Impact des modifications mineures et majeures

L'impacte de la modification d'un élément quelconque dépend du nombre de références faites à ce même élément.

Aussi paradoxal et controversable que cela puisse paraître, dans la plupart des cas, les modifications mineures demandent plus de refactorisations que les modifications majeures. Prenont comme exemple la méthode `Insert()`  de classe `Table`, elle est primordiale dans presque toutes les méthodes de tests de la classe `Table` car l'insertion est, en plusieurs sortes, implicite dans les fonctions **CRUD**; Elle est principale dans la création, et elle précéde la lecture, la mise-à-jour et la suppression. Vous noterez qu'elle est invoquée explicitement ou implicitement dans **toutes** les méthodes de tests de la classe `TableTest` soient les méthodes  `Insert()`, `Find()`, `FindAll()`, `FindWhere()`, `FirstWhere()`, `UpdateAll()`, `UpdateWhere()`, `Remove()` et `RemoveWhere()`. le retrait de cette méthode ou l'ajout ou le retrait d'un de ses paramètres nécessiterait la refactorisation de toutes ces enumérées méthodes.

En ce qui concerne la modifcation interne des méthodes et les modifications majeures telles que l'extension de classes, aucune refactorisation n'a été nécessaire ce qui est le cas de presque toutes les méthodes et fonctions d'un programme suivant le principe de la séparation des préoccupations (en anglais, **separation of concerns**).
