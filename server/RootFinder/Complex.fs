namespace RootFinder

//#nowarn "386"
open System
open System

//https://stackoverflow.com/a/31283508/4875161
[<NoComparison>]
[<CustomEquality>]
type Complex =
  struct
    val real: float
    val imag: float
    new (real, imag) =
      { real = real; imag = imag; }
  end

  member inline z.conjugate =
    Complex (z.real, -z.imag)

  member inline z.norm_squared =
    z.real * z.real + z.imag * z.imag;

  member inline z.norm =
    sqrt z.norm_squared

  member inline z.phase =
    Math.Atan2 (z.imag, z.real)

  override z.ToString() =
    let sign =
      if Math.Sign z.imag >= 0
      then '+'
      else '-'
    String.Format ("{0} {2} {1}*i", Math.Round(z.real, 10), Math.Round(Math.Abs z.imag, 10), sign)

  static member inline op_Explicit (z: Complex) = z
  static member inline op_Explicit (d: float) = Complex (float d, 0.0)
  static member inline op_Explicit (f: float32) = Complex (float f, 0.0)
  static member inline op_Explicit (n: int) = Complex (float n, 0.0)

  static member inline zero = Complex (0.0, 0.0)
  static member inline one = Complex (1.0, 0.0)

  // Unary negation
  static member inline (~-) (z: Complex) =
    Complex (-z.real, -z.imag)

  // Scalar operations
  static member inline (*) (s: ^T, z: Complex) =
    Complex (float s * z.real, float s * z.imag)

  static member inline (/) (z: Complex, s: ^T) =
    Complex (z.real / float s, z.imag / float s)

  // Field operations
  static member inline (+) (z1: Complex, z2: Complex) =
    Complex (z1.real + z2.real, z1.imag + z2.imag);

  static member inline (-) (z1: Complex, z2: Complex) =
    Complex (z1.real - z2.real, z1.imag - z2.imag);

  static member inline (*) (z1: Complex, z2: Complex) =
    Complex (z1.real * z2.real - z1.imag * z2.imag, z1.real * z2.imag + z1.imag * z2.real);

  static member inline (/) (z1: Complex, z2: Complex) =
    (z1 * z2.conjugate) / z2.norm_squared

  static member inline default_tolerance = 1e-10

  static member inline equal_within delta (z: Complex) (w: Complex) =
     (z - w).norm_squared <= delta

  interface IEquatable<Complex> with
    member z.Equals w =
      Complex.equal_within Complex.default_tolerance z w

[<AutoOpen>]
module Complex =
  // Infix constructor. Looks vaguely like '+ i'.
  let inline (+|) x  y =
    Complex (float x, float y)

  let inline complex d =
    d +| 0

  let inline polar (r: ^T) theta =
    r * (Math.Cos theta +| Math.Sin theta)

  let inline equal_within delta z w =
    Complex.equal_within delta z w
