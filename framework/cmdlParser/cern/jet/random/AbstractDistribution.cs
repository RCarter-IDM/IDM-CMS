/*
Copyright � 1999 CERN - European Organization for Nuclear Research.
Permission to use, copy, modify, distribute and sell this software and its documentation for any purpose
is hereby granted without fee, provided that the above copyright notice appear in all copies and
that both that copyright notice and this permission notice appear in supporting documentation.
CERN makes no representations about the suitability of this software for any purpose.
It is provided "as is" without expressed or implied warranty.*/
using System;
using RandomEngine = cern.jet.random.engine.RandomEngine;
namespace cern.jet.random
{
	/// <summary> Abstract base class for all random distributions.
	///
	/// A subclass of this class need to override method [tt]nextDouble()[/tt] and, in rare cases, also [tt]nextInt()[/tt].
	/// [p]
	/// Currently all subclasses use a uniform pseudo-random number generation engine and transform its results to the target distribution.
	/// Thus, they expect such a uniform engine upon instance construction.
	/// [p]
	/// {@link cern.jet.random.engine.MersenneTwister} is recommended as uniform pseudo-random number generation engine, since it is very strong and at the same time quick.
	/// {@link #makeDefaultGenerator()} will conveniently construct and return such a magic thing.
	/// You can also, for example, use {@link cern.jet.random.engine.DRand}, a quicker (but much weaker) uniform random number generation engine.
	/// Of course, you can also use other strong uniform random number generation engines.
	///
	/// [p]
	/// [b]Ressources on the Web:[/b]
	/// [dt]Check the Web version of the [A HREF="http://www.cern.ch/RD11/rkb/AN16pp/node1.html"] CERN Data Analysis Briefbook [/A]. This will clarify the definitions of most distributions.
	/// [dt]Also consult the [A HREF="http://www.statsoftinc.com/textbook/stathome.html"] StatSoft Electronic Textbook[/A] - the definite web book.
	/// [p]
	/// [b]Other useful ressources:[/b]
	/// [dt][A HREF="http://www.stats.gla.ac.uk/steps/glossary/probability_distributions.html"] Another site [/A] and [A HREF="http://www.statlets.com/usermanual/glossary.htm"] yet another site [/A]describing the definitions of several distributions.
	/// [dt]You may want to check out a [A HREF="http://www.stat.berkeley.edu/users/stark/SticiGui/Text/gloss.htm"] Glossary of Statistical Terms[/A].
	/// [dt]The GNU Scientific Library contains an extensive (but hardly readable) [A HREF="http://sourceware.cygnus.com/gsl/html/gsl-ref_toc.html#TOC26"] list of definition of distributions[/A].
	/// [dt]Use this Web interface to [A HREF="http://www.stat.ucla.edu/calculators/cdf"] plot all sort of distributions[/A].
	/// [dt]Even more ressources: [A HREF="http://www.animatedsoftware.com/statglos/statglos.htm"] Internet glossary of Statistical Terms[/A],
	/// [A HREF="http://www.ruf.rice.edu/~lane/hyperstat/index.html"] a text book[/A],
	/// [A HREF="http://www.stat.umn.edu/~jkuhn/courses/stat3091f/stat3091f.html"] another text book[/A].
	/// [dt]Finally, a good link list [A HREF="http://www.execpc.com/~helberg/statistics.html"] Statistics on the Web[/A].
	/// [p]
	/// </summary>
	/// <seealso cref="cern.jet.random.engine">
	/// </seealso>
	/// <seealso cref="cern.jet.random.engine.Benchmark">
	/// </seealso>
	/// <seealso cref="cern.jet.random.Benchmark">
	/// </seealso>
	/// <author>  wolfgang.hoschek@cern.ch
	/// </author>
	/// <version>  1.0, 09/24/99
	/// </version>
	[Serializable]
	public abstract class AbstractDistribution:cern.colt.PersistentObject, cern.colt.function.DoubleFunction, cern.colt.function.IntFunction, System.ICloneable
	{
		/// <summary> Returns the used uniform random number generator;</summary>
		/// <summary> Sets the uniform random generator internally used.</summary>
		virtual protected internal RandomEngine RandomGenerator
		{
			get
			{
				return randomGenerator;
			}

			set
			{
				this.randomGenerator = value;
			}

		}
		protected internal RandomEngine randomGenerator;
		/// <summary> Makes this class non instantiable, but still let's others inherit from it.</summary>
		protected internal AbstractDistribution()
		{
		}
		/// <summary>Equivalent to [tt]nextDouble()[/tt].
		/// This has the effect that distributions can now be used as function objects, returning a random number upon function evaluation.
		/// </summary>
		public virtual double apply(double dummy)
		{
			return nextDouble();
		}
		/// <summary>Equivalent to [tt]nextInt()[/tt].
		/// This has the effect that distributions can now be used as function objects, returning a random number upon function evaluation.
		/// </summary>
		public virtual int apply(int dummy)
		{
			return nextInt();
		}
		/// <summary> Returns a deep copy of the receiver; the copy will produce identical sequences.
		/// After this call has returned, the copy and the receiver have equal but separate state.
		///
		/// </summary>
		/// <returns> a copy of the receiver.
		/// </returns>
		public override System.Object Clone()
		{
			AbstractDistribution copy = (AbstractDistribution) base.Clone();
			if (this.randomGenerator != null)
				copy.randomGenerator = (RandomEngine) this.randomGenerator.Clone();
			return copy;
		}
		/// <summary> Constructs and returns a new uniform random number generation engine seeded with the current time.
		/// Currently this is {@link cern.jet.random.engine.MersenneTwister}.
		/// </summary>
		public static RandomEngine makeDefaultGenerator()
		{
			return cern.jet.random.engine.RandomEngine.makeDefault();
		}
		/// <summary> Returns a random number from the distribution.</summary>
		public abstract double nextDouble();
		/// <summary> Returns a random number from the distribution; returns [tt](int) Math.round(nextDouble())[/tt].
		/// Override this method if necessary.
		/// </summary>
		public virtual int nextInt()
		{
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			return (int) System.Math.Round(nextDouble());
		}
	}
}