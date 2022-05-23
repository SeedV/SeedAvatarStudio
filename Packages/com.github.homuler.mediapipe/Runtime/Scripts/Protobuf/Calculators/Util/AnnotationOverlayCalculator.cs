// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/calculators/util/annotation_overlay_calculator.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe {

  /// <summary>Holder for reflection information generated from mediapipe/calculators/util/annotation_overlay_calculator.proto</summary>
  public static partial class AnnotationOverlayCalculatorReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/calculators/util/annotation_overlay_calculator.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AnnotationOverlayCalculatorReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cj5tZWRpYXBpcGUvY2FsY3VsYXRvcnMvdXRpbC9hbm5vdGF0aW9uX292ZXJs",
            "YXlfY2FsY3VsYXRvci5wcm90bxIJbWVkaWFwaXBlGiRtZWRpYXBpcGUvZnJh",
            "bWV3b3JrL2NhbGN1bGF0b3IucHJvdG8aGm1lZGlhcGlwZS91dGlsL2NvbG9y",
            "LnByb3RvItICCiJBbm5vdGF0aW9uT3ZlcmxheUNhbGN1bGF0b3JPcHRpb25z",
            "Eh0KD2NhbnZhc193aWR0aF9weBgCIAEoBToEMTkyMBIeChBjYW52YXNfaGVp",
            "Z2h0X3B4GAMgASgFOgQxMDgwEiYKDGNhbnZhc19jb2xvchgEIAEoCzIQLm1l",
            "ZGlhcGlwZS5Db2xvchIjChRmbGlwX3RleHRfdmVydGljYWxseRgFIAEoCDoF",
            "ZmFsc2USJgoYZ3B1X3VzZXNfdG9wX2xlZnRfb3JpZ2luGAYgASgIOgR0cnVl",
            "EhsKEGdwdV9zY2FsZV9mYWN0b3IYByABKAI6ATEyWwoDZXh0EhwubWVkaWFw",
            "aXBlLkNhbGN1bGF0b3JPcHRpb25zGIfwv3cgASgLMi0ubWVkaWFwaXBlLkFu",
            "bm90YXRpb25PdmVybGF5Q2FsY3VsYXRvck9wdGlvbnM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.CalculatorReflection.Descriptor, global::Mediapipe.ColorReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.AnnotationOverlayCalculatorOptions), global::Mediapipe.AnnotationOverlayCalculatorOptions.Parser, new[]{ "CanvasWidthPx", "CanvasHeightPx", "CanvasColor", "FlipTextVertically", "GpuUsesTopLeftOrigin", "GpuScaleFactor" }, null, null, new pb::Extension[] { global::Mediapipe.AnnotationOverlayCalculatorOptions.Extensions.Ext }, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Options for the AnnotationOverlayCalculator.
  /// </summary>
  public sealed partial class AnnotationOverlayCalculatorOptions : pb::IMessage<AnnotationOverlayCalculatorOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<AnnotationOverlayCalculatorOptions> _parser = new pb::MessageParser<AnnotationOverlayCalculatorOptions>(() => new AnnotationOverlayCalculatorOptions());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<AnnotationOverlayCalculatorOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.AnnotationOverlayCalculatorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnnotationOverlayCalculatorOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnnotationOverlayCalculatorOptions(AnnotationOverlayCalculatorOptions other) : this() {
      _hasBits0 = other._hasBits0;
      canvasWidthPx_ = other.canvasWidthPx_;
      canvasHeightPx_ = other.canvasHeightPx_;
      canvasColor_ = other.canvasColor_ != null ? other.canvasColor_.Clone() : null;
      flipTextVertically_ = other.flipTextVertically_;
      gpuUsesTopLeftOrigin_ = other.gpuUsesTopLeftOrigin_;
      gpuScaleFactor_ = other.gpuScaleFactor_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AnnotationOverlayCalculatorOptions Clone() {
      return new AnnotationOverlayCalculatorOptions(this);
    }

    /// <summary>Field number for the "canvas_width_px" field.</summary>
    public const int CanvasWidthPxFieldNumber = 2;
    private readonly static int CanvasWidthPxDefaultValue = 1920;

    private int canvasWidthPx_;
    /// <summary>
    /// The canvas width and height in pixels, and the background color. These
    /// options are used only if an input stream of ImageFrame isn't provided to
    /// the renderer calculator. If an input stream of ImageFrame is provided, then
    /// the calculator renders the annotations on top of the provided image, else a
    /// canvas is created with the dimensions and background color specified in
    /// these options and the annotations are rendered on top of this canvas.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CanvasWidthPx {
      get { if ((_hasBits0 & 1) != 0) { return canvasWidthPx_; } else { return CanvasWidthPxDefaultValue; } }
      set {
        _hasBits0 |= 1;
        canvasWidthPx_ = value;
      }
    }
    /// <summary>Gets whether the "canvas_width_px" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasCanvasWidthPx {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "canvas_width_px" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearCanvasWidthPx() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "canvas_height_px" field.</summary>
    public const int CanvasHeightPxFieldNumber = 3;
    private readonly static int CanvasHeightPxDefaultValue = 1080;

    private int canvasHeightPx_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CanvasHeightPx {
      get { if ((_hasBits0 & 2) != 0) { return canvasHeightPx_; } else { return CanvasHeightPxDefaultValue; } }
      set {
        _hasBits0 |= 2;
        canvasHeightPx_ = value;
      }
    }
    /// <summary>Gets whether the "canvas_height_px" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasCanvasHeightPx {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "canvas_height_px" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearCanvasHeightPx() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "canvas_color" field.</summary>
    public const int CanvasColorFieldNumber = 4;
    private global::Mediapipe.Color canvasColor_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Mediapipe.Color CanvasColor {
      get { return canvasColor_; }
      set {
        canvasColor_ = value;
      }
    }

    /// <summary>Field number for the "flip_text_vertically" field.</summary>
    public const int FlipTextVerticallyFieldNumber = 5;
    private readonly static bool FlipTextVerticallyDefaultValue = false;

    private bool flipTextVertically_;
    /// <summary>
    /// Whether text should be rendered upside down. When it's set to false, text
    /// is rendered normally assuming the underlying image has its origin at the
    /// top-left corner. Therefore, for images with the origin at the bottom-left
    /// corner this should be set to true.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool FlipTextVertically {
      get { if ((_hasBits0 & 4) != 0) { return flipTextVertically_; } else { return FlipTextVerticallyDefaultValue; } }
      set {
        _hasBits0 |= 4;
        flipTextVertically_ = value;
      }
    }
    /// <summary>Gets whether the "flip_text_vertically" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasFlipTextVertically {
      get { return (_hasBits0 & 4) != 0; }
    }
    /// <summary>Clears the value of the "flip_text_vertically" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearFlipTextVertically() {
      _hasBits0 &= ~4;
    }

    /// <summary>Field number for the "gpu_uses_top_left_origin" field.</summary>
    public const int GpuUsesTopLeftOriginFieldNumber = 6;
    private readonly static bool GpuUsesTopLeftOriginDefaultValue = true;

    private bool gpuUsesTopLeftOrigin_;
    /// <summary>
    /// Whether input stream IMAGE_GPU (OpenGL texture) has bottom-left or top-left
    /// origin. (Historically, OpenGL uses bottom left origin, but most MediaPipe
    /// examples expect textures to have top-left origin.)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool GpuUsesTopLeftOrigin {
      get { if ((_hasBits0 & 8) != 0) { return gpuUsesTopLeftOrigin_; } else { return GpuUsesTopLeftOriginDefaultValue; } }
      set {
        _hasBits0 |= 8;
        gpuUsesTopLeftOrigin_ = value;
      }
    }
    /// <summary>Gets whether the "gpu_uses_top_left_origin" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasGpuUsesTopLeftOrigin {
      get { return (_hasBits0 & 8) != 0; }
    }
    /// <summary>Clears the value of the "gpu_uses_top_left_origin" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearGpuUsesTopLeftOrigin() {
      _hasBits0 &= ~8;
    }

    /// <summary>Field number for the "gpu_scale_factor" field.</summary>
    public const int GpuScaleFactorFieldNumber = 7;
    private readonly static float GpuScaleFactorDefaultValue = 1F;

    private float gpuScaleFactor_;
    /// <summary>
    /// Scale factor for intermediate image for GPU rendering.
    /// This can be used to speed up annotation by drawing the annotation on an
    /// intermediate image with a reduced scale, e.g. 0.5 (of the input image width
    /// and height), before resizing and overlaying it on top of the input image.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float GpuScaleFactor {
      get { if ((_hasBits0 & 16) != 0) { return gpuScaleFactor_; } else { return GpuScaleFactorDefaultValue; } }
      set {
        _hasBits0 |= 16;
        gpuScaleFactor_ = value;
      }
    }
    /// <summary>Gets whether the "gpu_scale_factor" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasGpuScaleFactor {
      get { return (_hasBits0 & 16) != 0; }
    }
    /// <summary>Clears the value of the "gpu_scale_factor" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearGpuScaleFactor() {
      _hasBits0 &= ~16;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as AnnotationOverlayCalculatorOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AnnotationOverlayCalculatorOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (CanvasWidthPx != other.CanvasWidthPx) return false;
      if (CanvasHeightPx != other.CanvasHeightPx) return false;
      if (!object.Equals(CanvasColor, other.CanvasColor)) return false;
      if (FlipTextVertically != other.FlipTextVertically) return false;
      if (GpuUsesTopLeftOrigin != other.GpuUsesTopLeftOrigin) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(GpuScaleFactor, other.GpuScaleFactor)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasCanvasWidthPx) hash ^= CanvasWidthPx.GetHashCode();
      if (HasCanvasHeightPx) hash ^= CanvasHeightPx.GetHashCode();
      if (canvasColor_ != null) hash ^= CanvasColor.GetHashCode();
      if (HasFlipTextVertically) hash ^= FlipTextVertically.GetHashCode();
      if (HasGpuUsesTopLeftOrigin) hash ^= GpuUsesTopLeftOrigin.GetHashCode();
      if (HasGpuScaleFactor) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(GpuScaleFactor);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (HasCanvasWidthPx) {
        output.WriteRawTag(16);
        output.WriteInt32(CanvasWidthPx);
      }
      if (HasCanvasHeightPx) {
        output.WriteRawTag(24);
        output.WriteInt32(CanvasHeightPx);
      }
      if (canvasColor_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(CanvasColor);
      }
      if (HasFlipTextVertically) {
        output.WriteRawTag(40);
        output.WriteBool(FlipTextVertically);
      }
      if (HasGpuUsesTopLeftOrigin) {
        output.WriteRawTag(48);
        output.WriteBool(GpuUsesTopLeftOrigin);
      }
      if (HasGpuScaleFactor) {
        output.WriteRawTag(61);
        output.WriteFloat(GpuScaleFactor);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (HasCanvasWidthPx) {
        output.WriteRawTag(16);
        output.WriteInt32(CanvasWidthPx);
      }
      if (HasCanvasHeightPx) {
        output.WriteRawTag(24);
        output.WriteInt32(CanvasHeightPx);
      }
      if (canvasColor_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(CanvasColor);
      }
      if (HasFlipTextVertically) {
        output.WriteRawTag(40);
        output.WriteBool(FlipTextVertically);
      }
      if (HasGpuUsesTopLeftOrigin) {
        output.WriteRawTag(48);
        output.WriteBool(GpuUsesTopLeftOrigin);
      }
      if (HasGpuScaleFactor) {
        output.WriteRawTag(61);
        output.WriteFloat(GpuScaleFactor);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (HasCanvasWidthPx) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(CanvasWidthPx);
      }
      if (HasCanvasHeightPx) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(CanvasHeightPx);
      }
      if (canvasColor_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(CanvasColor);
      }
      if (HasFlipTextVertically) {
        size += 1 + 1;
      }
      if (HasGpuUsesTopLeftOrigin) {
        size += 1 + 1;
      }
      if (HasGpuScaleFactor) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AnnotationOverlayCalculatorOptions other) {
      if (other == null) {
        return;
      }
      if (other.HasCanvasWidthPx) {
        CanvasWidthPx = other.CanvasWidthPx;
      }
      if (other.HasCanvasHeightPx) {
        CanvasHeightPx = other.CanvasHeightPx;
      }
      if (other.canvasColor_ != null) {
        if (canvasColor_ == null) {
          CanvasColor = new global::Mediapipe.Color();
        }
        CanvasColor.MergeFrom(other.CanvasColor);
      }
      if (other.HasFlipTextVertically) {
        FlipTextVertically = other.FlipTextVertically;
      }
      if (other.HasGpuUsesTopLeftOrigin) {
        GpuUsesTopLeftOrigin = other.GpuUsesTopLeftOrigin;
      }
      if (other.HasGpuScaleFactor) {
        GpuScaleFactor = other.GpuScaleFactor;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 16: {
            CanvasWidthPx = input.ReadInt32();
            break;
          }
          case 24: {
            CanvasHeightPx = input.ReadInt32();
            break;
          }
          case 34: {
            if (canvasColor_ == null) {
              CanvasColor = new global::Mediapipe.Color();
            }
            input.ReadMessage(CanvasColor);
            break;
          }
          case 40: {
            FlipTextVertically = input.ReadBool();
            break;
          }
          case 48: {
            GpuUsesTopLeftOrigin = input.ReadBool();
            break;
          }
          case 61: {
            GpuScaleFactor = input.ReadFloat();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 16: {
            CanvasWidthPx = input.ReadInt32();
            break;
          }
          case 24: {
            CanvasHeightPx = input.ReadInt32();
            break;
          }
          case 34: {
            if (canvasColor_ == null) {
              CanvasColor = new global::Mediapipe.Color();
            }
            input.ReadMessage(CanvasColor);
            break;
          }
          case 40: {
            FlipTextVertically = input.ReadBool();
            break;
          }
          case 48: {
            GpuUsesTopLeftOrigin = input.ReadBool();
            break;
          }
          case 61: {
            GpuScaleFactor = input.ReadFloat();
            break;
          }
        }
      }
    }
    #endif

    #region Extensions
    /// <summary>Container for extensions for other messages declared in the AnnotationOverlayCalculatorOptions message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Extensions {
      public static readonly pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.AnnotationOverlayCalculatorOptions> Ext =
        new pb::Extension<global::Mediapipe.CalculatorOptions, global::Mediapipe.AnnotationOverlayCalculatorOptions>(250607623, pb::FieldCodec.ForMessage(2004860986, global::Mediapipe.AnnotationOverlayCalculatorOptions.Parser));
    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code