/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace Grib.Api.Interop.SWIG {

public class grib_util_packing_spec : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal grib_util_packing_spec(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(grib_util_packing_spec obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~grib_util_packing_spec() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          GribApiProxyPINVOKE.delete_grib_util_packing_spec(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int packing_type {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_packing_type_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_packing_type_get(swigCPtr);
      return ret;
    } 
  }

  public int packing {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_packing_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_packing_get(swigCPtr);
      return ret;
    } 
  }

  public int boustrophedonic {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_boustrophedonic_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_boustrophedonic_get(swigCPtr);
      return ret;
    } 
  }

  public int editionNumber {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_editionNumber_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_editionNumber_get(swigCPtr);
      return ret;
    } 
  }

  public int accuracy {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_accuracy_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_accuracy_get(swigCPtr);
      return ret;
    } 
  }

  public int bitsPerValue {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_bitsPerValue_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_bitsPerValue_get(swigCPtr);
      return ret;
    } 
  }

  public int decimalScaleFactor {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_decimalScaleFactor_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_decimalScaleFactor_get(swigCPtr);
      return ret;
    } 
  }

  public int computeLaplacianOperator {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_computeLaplacianOperator_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_computeLaplacianOperator_get(swigCPtr);
      return ret;
    } 
  }

  public int truncateLaplacian {
	set
	{
		GribApiProxyPINVOKE.grib_util_packing_spec_truncateLaplacian_set(swigCPtr, value);
	} 
	get
	{
		return GribApiProxyPINVOKE.grib_util_packing_spec_truncateLaplacian_get(swigCPtr);
	} 
  }

  public double laplacianOperator {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_laplacianOperator_set(swigCPtr, value);
    } 
    get {
      double ret = GribApiProxyPINVOKE.grib_util_packing_spec_laplacianOperator_get(swigCPtr);
      return ret;
    } 
  }

  public int deleteLocalDefinition {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_deleteLocalDefinition_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_deleteLocalDefinition_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_grib_values extra_settings {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_extra_settings_set(swigCPtr, SWIGTYPE_p_grib_values.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = GribApiProxyPINVOKE.grib_util_packing_spec_extra_settings_get(swigCPtr);
      SWIGTYPE_p_grib_values ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_grib_values(cPtr, false);
      return ret;
    } 
  }

  public int extra_settings_count {
    set {
      GribApiProxyPINVOKE.grib_util_packing_spec_extra_settings_count_set(swigCPtr, value);
    } 
    get {
      int ret = GribApiProxyPINVOKE.grib_util_packing_spec_extra_settings_count_get(swigCPtr);
      return ret;
    } 
  }

  public grib_util_packing_spec() : this(GribApiProxyPINVOKE.new_grib_util_packing_spec(), true) {
  }

}

}
