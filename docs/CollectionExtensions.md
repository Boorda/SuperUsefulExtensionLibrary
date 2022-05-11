## CollectionExtensions

Extensions to collections or enumerables in the **System.Collections** Namespace.

**Namespace** *SUELIB.CollectionExtensions*

**Dependencies**

- System
- System.Collections.Concurrent
- System.Collections.Generic
- System.Linq

---

### AddDistinct\<T\>

 Adds an item to an ICollection\<T\> if the item does not already exist in the collection.

 Returns **true** it the object was added successfully.

| Parameter | Description |
|-----------|-------------|
|**T**|The Type contained in the collection. |
|**collection**|The collection to be modified.|
|**item**|The Object to be added to the collection.|

#### Method 

```
public static bool AddDistinct<T>(this ICollection<T> collection, T item)
{
    bool added = false;
    if (!collection.Contains(item)) { collection.Add(item); added = true; }
    return added;
}
```
 ---

### AddRangeDistinct\<T\>

Adds an object only if the object does not already exist in the collection.

Returns an **int** representing the number of items successfully added to the collection.

| Parameter | Description |
|-----------|-------------|
|**T**|The Type of object contained within the collection.|
|**collection**|The collection to be modified.|
|**range**|The range of Objects to be added to the collection.|

#### Method 

```
public static int AddRangeDistinct<T>(this ICollection<T> collection, ICollection<T> range) where T : class
{
    if (collection is null || range is null) { return 0; }
    int itemsAdded = 0;
    foreach (T item in range)
    {
        if (!collection.Contains(item)) { collection.Add(item); itemsAdded++; }
    }
    return itemsAdded;
}
```
---

### AddRange\<T\>

Adds a group of objects the collection. 

Returns an **int** representing the number of items successfully added to the collection.

| Parameter | Description |
|-----------|-------------|
|**T**|The Type of object contained within the collection.|
|**collection**|The ConcurrentBag\<T\> to be modified.|
|**range**|The range of Objects to be added to the collection.|

#### Method

```
public static int AddRange<T>(this ConcurrentBag<T> collection, IEnumerable<T> range) where T : class
{
    if (collection is null || range is null) { return 0; }
    int itemsAdded = 0;
    foreach (T item in range)
    {
        collection.Add(item);
        itemsAdded++;
    }
    return itemsAdded;
}
```

---

### AddDistinct\<T\> 

Adds an item to an ConcurrentBag\<T\> if the item does not already exist in the collection.

Returns **true** it the object was added successfully.


| Parameter | Description |
|-----------|-------------|
|**T**|The Type of object contained within the collection.|
|**collection**|The ConcurrentBag\<T\> to be modified.|
|**item**|The Object to be added to the collection.|

#### Method

```
public static bool AddDistinct<T>(this ConcurrentBag<T> collection, T item)
{
    bool added = false;
    if (!collection.Contains(item)) { collection.Add(item); added = true; }
    return added;
}
```

---

### AddRangeDistinct\<T\>

Adds an object only if the object does not already exist in the ConcurrentBag\<T\> collection.

Returns an **int** representing the number of items successfully added to the collection.

| Parameter | Description |
|-----------|-------------|
|**T**|The Type of object contained within the collection.|
|**collection**|The ConcurrentBag\<T\> to be modified.|
|**range**|The range of Objects to be added to the collection.|

#### Method 

```
public static int AddRangeDistinct<T>(this ConcurrentBag<T> collection, IEnumerable<T> range) where T : class
{
    if (collection is null || range is null) { return 0; }
    int itemsAdded = 0;
    foreach (T item in range)
    {
        if (!collection.Contains(item)) { collection.Add(item); itemsAdded++; }
    }
    return itemsAdded;
}
```

 ---

### ICollection\<T\> Merge\<T\>

Merges up to two additional collections together with the current collection and returns a single collection with only one instance of each collection item from any of the collections. Whew!

| Parameter | Description |
|-----------|-------------|
|**T**|The Type of object contained within the collection.|
|**collection**|The ICollection\<T\> to add the new collection to.|
|**collectionA**|An ICollection\<T\> to be added.|
|**collectionB**|An optional second ICollection\<T\> to be added.|

 #### Method 

```
public static ICollection<T> Merge<T>(this ICollection<T> collection, ICollection<T> collectionA, ICollection<T> collectionB = null) where T : class
{
    if (collectionA is null && collectionB is null) { throw new ArgumentNullException("You must provide at least one collection for the merge command."); }
    ICollection<T> tempCol = new List<T>(collection);
    if (collectionA != null) { _ = tempCol.AddRangeDistinct(collectionA); }
    if (collectionB != null) { _ = tempCol.AddRangeDistinct(collectionB); }

    return tempCol;
}
```
