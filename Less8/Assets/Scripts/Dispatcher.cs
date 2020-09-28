using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    public int x = 11;
    public static Dispatcher instance;

    public Dispatcher()
    {
        x = 12;
    }

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
		DontDestroyOnLoad(instance.gameObject);
		instance.StaticStart();
	}

    public void CloseApp()
    {
		UnityMainThreadDispatcher.Instance().Invoke("StaticStart", 0f);
    }

    public void StaticStart()
    {
        var seconds = 2;
        Debug.LogError("DON'T CHANGE MY REPOSITORY!");
        Task.Factory.StartNew(() => {
            //Debug.LogError("THE APP WILL BE CLOSED AFTER");
            for (int i = 0; i <= seconds; i++)
            {
                Thread.Sleep(1000);
                Debug.LogError(seconds - i);
            }
			//Debug.LogError("CLOSING APP..");
			//EditorApplication.ExecuteMenuItem("Edit/Play");
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
		});
    }
}



public class UnityMainThreadDispatcher : MonoBehaviour
{

	private static readonly Queue<Action> _executionQueue = new Queue<Action>();

	public void Update()
	{
		lock (_executionQueue)
		{
			while (_executionQueue.Count > 0)
			{
				_executionQueue.Dequeue().Invoke();
			}
		}
	}

	/// <summary>
	/// Locks the queue and adds the IEnumerator to the queue
	/// </summary>
	/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
	public void Enqueue(IEnumerator action)
	{
		lock (_executionQueue)
		{
			_executionQueue.Enqueue(() => {
				StartCoroutine(action);
			});
		}
	}

	/// <summary>
	/// Locks the queue and adds the Action to the queue
	/// </summary>
	/// <param name="action">function that will be executed from the main thread.</param>
	public void Enqueue(Action action)
	{
		Enqueue(ActionWrapper(action));
	}

	/// <summary>
	/// Locks the queue and adds the Action to the queue, returning a Task which is completed when the action completes
	/// </summary>
	/// <param name="action">function that will be executed from the main thread.</param>
	/// <returns>A Task that can be awaited until the action completes</returns>
	public Task EnqueueAsync(Action action)
	{
		var tcs = new TaskCompletionSource<bool>();

		void WrappedAction()
		{
			try
			{
				action();
				tcs.TrySetResult(true);
			}
			catch (Exception ex)
			{
				tcs.TrySetException(ex);
			}
		}

		Enqueue(ActionWrapper(WrappedAction));
		return tcs.Task;
	}


	IEnumerator ActionWrapper(Action a)
	{
		a();
		yield return null;
	}


	private static UnityMainThreadDispatcher _instance = null;

	public static bool Exists()
	{
		return _instance != null;
	}

	public static UnityMainThreadDispatcher Instance()
	{
		if (!Exists())
		{
			throw new Exception("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
		}
		return _instance;
	}


	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	void OnDestroy()
	{
		_instance = null;
	}


}
