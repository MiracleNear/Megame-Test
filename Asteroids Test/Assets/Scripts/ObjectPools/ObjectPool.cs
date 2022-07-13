using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPools
{
    public abstract class ObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour
    {
         [SerializeField] private int _baseCapacity = 16;
         [SerializeField] private int _additionCapacity = 16;
         [SerializeField] private T _template;
         
         private List<PoolElement> _pool;

         private void Awake()
         {
             CreatePool();
         }

         private void CreatePool()
         {
             _pool = new List<PoolElement>(_baseCapacity);
             
             SpawnElements(_baseCapacity);
         }
         
         private void SpawnElements(int count)
         {
             for (int i = 0; i < count; i++)
             {
                 T template = Instantiate(_template, transform);

                 template.gameObject.SetActive(false);

                 PoolElement poolElement = new PoolElement(template);
                 
                 _pool.Add(poolElement);
             }
         }

         public T GetFreeElement()
         {
             PoolElement poolElement = _pool.FirstOrDefault(poolElement => poolElement.IsUsing == false);

             if (poolElement != null)
             {
                 GameObject gameObject = poolElement.Template.gameObject;
                 
                 gameObject.SetActive(true);
                 gameObject.transform.SetParent(null);
                 poolElement.IsUsing = true;

                 return poolElement.Template;
             }
             else
             {
                 SpawnElements(_additionCapacity);
                 return GetFreeElement();
             }
         }

         public void ReturnToPool(T element)
         {
             PoolElement poolElement = _pool.FirstOrDefault(poolElement => poolElement.Template == element);

             if (poolElement != null)
             {
                 poolElement.IsUsing = false;
                 element.gameObject.SetActive(false);
                 element.transform.SetParent(transform);
             }
         }
         
         

         private class PoolElement
         {
             public T Template;
             public bool IsUsing;

             public PoolElement(T template)
             {
                 Template = template;
             }
             
         }
    }
}