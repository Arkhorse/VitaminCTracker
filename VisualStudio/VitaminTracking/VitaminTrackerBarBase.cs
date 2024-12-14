//using UnityEngine.Events;

//namespace VitaminCTracker
//{
//	[RegisterTypeInIl2Cpp(false)]
//	public class VitaminTrackerBarBase : MonoBehaviour
//	{
//		/// <summary>Used to display what the name of this bar is</summary>
//		string _Name;

//		/// <summary></summary>
//		float _Min;
//		/// <summary></summary>
//		float _Max;
//		/// <summary></summary>
//		float _Value;

//		/// <summary></summary>
//		UnityEvent<float> _OnValueChangedEvent;

//		public string? Name
//		{
//			get => _Name;
//			set
//			{
//				_Name = Name;
//			}
//		}
//		public float Min
//		{
//			get => _Min;
//			set
//			{
//				_Min = value;
//				if (Value < _Min) Value = _Min;
//			}
//		}
//		public float Max
//		{
//			get => _Max;
//			set
//			{
//				_Max = _Value;
//				if (Value > _Max) Value = _Max;
//			}
//		}
//		public float Value
//		{
//			get => _Value;
//			set
//			{

//			}
//		}
//		public UnityEvent<float> OnValueChangedEvent => _OnValueChangedEvent;

//		protected virtual void OnValueChanging(ref float value) { }
//		protected virtual void OnValueChanged(float value) { }

//		public void SetValueWithoutNotification(float value)
//		{
//			OnValueChanging(ref value);
//			this._Value = value;
//			OnValueChanged(value);
//		}
//		public void ForceNotify()
//		{
//			SetValue(_Value);
//		}

//		void SetValue(float value)
//		{
//			OnValueChanging(ref value);
//			this._Value = value;
//			OnValueChanged(value);

//			_OnValueChangedEvent.Invoke(value);
//		}
//		protected float GetNormalizedValue()
//		{
//			return Mathf.InverseLerp(Min, Max, _Value);
//		}
//	}
//}
