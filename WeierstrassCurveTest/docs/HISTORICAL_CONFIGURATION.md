# Historical configuration of ECDLP benchmark experiments

This document records the confirmed, inferred, and unknown configuration details associated with the benchmark experiments reported in the manuscript:

**“Analysing Methods of Solving the Discrete Logarithm Problem on Elliptic Curves over Finite Fields.”**

The goal of this file is not to claim exact bit-for-bit replay of the historical experiments. Instead, it documents what can be stated about the benchmark configuration with the available artefacts and distinguishes historical benchmark assumptions from the current maintained source code.

## 1. Scope and interpretation

The manuscript reports aggregate results for five base solvers evaluated within a common Pohlig-Hellman decomposition workflow over composite-order cyclic target subgroups.

The benchmark datasets associated with the manuscript are the files with the `com-` prefix:

```text
WeierstrassCurveTest/Performance/Datasets/com-data25.csv
WeierstrassCurveTest/Performance/Datasets/com-data26.csv
WeierstrassCurveTest/Performance/Datasets/com-data27.csv
WeierstrassCurveTest/Performance/Datasets/com-data28.csv
WeierstrassCurveTest/Performance/Datasets/com-data29.csv
WeierstrassCurveTest/Performance/Datasets/com-data30.csv
```

The current `master` branch contains a maintained development implementation. It may include changes made after the original benchmark runs. Therefore, this file distinguishes between:

- **confirmed historical configuration**: directly supported by archived datasets, recorded outputs, or stable project design;
- **inferred configuration**: consistent with the maintained implementation but not independently proven to be the exact historical setting;
- **unknown or not recorded**: details that were not preserved during the original experimental phase.

## 2. Dataset-level configuration

| Item | Status | Configuration / note |
|---|---|---|
| Dataset family used for manuscript table | Confirmed | `com-data25.csv` through `com-data30.csv`. |
| Curve model | Confirmed | Short-Weierstrass curves over prime finite fields. |
| Field sizes | Confirmed from manuscript dataset naming and manuscript text | Six datasets corresponding to field-size targets 25, 26, 27, 28, 29, and 30 bits. |
| Target subgroup | Confirmed by manuscript model | Each benchmark instance is interpreted in the cyclic subgroup `G = <P>`, with `N = ord(P)`. |
| Target subgroup order | Confirmed by manuscript model | Composite `N = ord(P)`, intended for Pohlig-Hellman decomposition. |
| Dataset schema | Confirmed | Eleven-column CSV: `p, A, B, curve_order, P_x, P_y, P_order, Q_x, Q_y, Q_order, expected_k`. |
| CSV header | Confirmed by manuscript description | Primary benchmark CSV files are described as being written without a header. |
| Historical random seeds | Not recorded | The original data-generation process did not preserve random seeds. The repository should not claim bit-for-bit regeneration of historical datasets. |

## 3. Evaluation-level configuration

| Item | Status | Configuration / note |
|---|---|---|
| Main comparison model | Confirmed by manuscript revision | Five base solvers are compared inside a common Pohlig-Hellman workflow. |
| Compared base solvers | Confirmed | Shanks' Baby-step Giant-step, Pollard's Rho, Kangaroo, Bernstein-Lange / Two Grumpy Giants and a Baby, and Las Vegas. |
| Pohlig-Hellman role | Confirmed by manuscript revision | Decomposition wrapper, not a sixth peer solver. |
| Prime-order production ECC interpretation | Confirmed limitation | The reported results should not be interpreted as a direct security assessment of production-grade prime-order ECC. |
| Historical solver seeds | Not recorded | Stochastic solver runs did not preserve random seeds. Re-running probabilistic solvers may produce different walks and low-level timing results. |
| Parameter sweep | Not documented | The manuscript should not claim exhaustive parameter optimisation or a systematic parameter-sensitivity study unless separate evidence is provided. |
| Negation-map options | Confirmed unused for historical Table 1 | The maintained implementation supports both negation-map and extended-negation-map flags, but neither option was used during the benchmark runs reported in Table 1. |
| Worker count | Partially confirmed / needs manuscript alignment | The manuscript states twelve threads. The maintained implementation computes worker count from `Environment.ProcessorCount - 5`. Use the manuscript value only if it reflects the benchmark machine used for Table 1. |
| Warm-up / heating phase | Confirmed from maintained implementation and manuscript | First 5% of records are processed as warm-up and excluded from recorded results. |
| Records retained per 100,000-row dataset | Confirmed by manuscript logic | 95,000 measured rows after excluding 5% warm-up. |

## 4. Maintained implementation notes

The following details are derived from the maintained `master` implementation. They are useful for understanding the current reference implementation but should be treated as **inferred historical settings** unless independently confirmed by old logs, result files, source snapshots, or commit evidence.

### 4.1. Base class and effective subgroup order

Current implementation pattern:

```text
DLPMethod.SetModulo(modulo)
```

The maintained implementation contains a `modulo` field intended to represent the effective order used by a base solver. In the Pohlig-Hellman workflow, the wrapper calls `SetModulo(p)` for digit-level subproblems of prime order `p`.

Historical confidence: **inferred**.

## 5. Pohlig-Hellman wrapper

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Factorisation of `N` | Trial division from 2 to `sqrt(N)`. | Inferred |
| Decomposition target | Prime-power factors of the target-subgroup order. | Confirmed conceptually |
| Subproblem solver | One selected base solver per benchmark configuration. | Confirmed conceptually |
| CRT reconstruction | Chinese Remainder Theorem over residues modulo prime-power factors. | Confirmed conceptually |
| Factorisation included in runtime | Claimed in manuscript text. Confirm only if benchmark measurement called the wrapper during timed execution. | Inferred / needs confirmation |

## 6. Shanks' Baby-step Giant-step

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Search parameter | `m = ceil(sqrt(M))`, where `M` is the effective order supplied to the solver. | Inferred |
| Baby steps | Stored list of multiples of `P`. | Inferred |
| Giant steps | Sequential search through `Q - j m P`. | Inferred |
| Randomness | None. | Inferred |
| Restart rule | None; deterministic search. | Inferred |
| Memory model | Stores the baby-step table. | Confirmed theoretically / inferred implementation |

## 7. Pollard's Rho

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Partition count | 20 partitions in the maintained implementation. | Inferred |
| Coefficients | Random coefficient pairs modulo the effective order. | Inferred |
| Collision detection | Block-style tortoise/hare schedule. | Inferred |
| Restart behaviour | Generates a new argument list after the block limit is exceeded or an uninformative collision is encountered. | Inferred |
| Iteration block limit | Maintained code uses `3 * sqrt(curve.Order())`; this may need correction to use the effective subproblem order. | Current-code note |
| Negation map | Supported by flags, Confirmed unused for historical Table 1. | Inferred |
| Extended negation map | Supported by flags, Confirmed unused for historical Table 1. | Inferred |
| Random seeds | Not recorded. | Not recorded |

## 8. Kangaroo method

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Interval | Maintained implementation sets `[a, b] = [1, M]`, where `M` is the effective order. | Inferred |
| `epsilon` | `sqrt(b - a)`. | Inferred |
| Tame steps coefficient | 0.7 in maintained implementation. | Inferred |
| Wild steps coefficient | 3.0 in maintained implementation. | Inferred |
| Trap-distance coefficient | 0.1 in maintained implementation. | Inferred |
| Jump mapping | Maintained implementation maps the x-coordinate modulo `hashModulo` to a generated step list. | Inferred |
| Step map randomness | Uses unseeded pseudorandom generation in maintained implementation. | Inferred / not historically reproducible |
| Parameter sweep | Not documented. | Not documented |

## 9. Bernstein-Lange / Two Grumpy Giants and a Baby

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Small-step parameter `n` | Maintained implementation uses `n = 1`. | Inferred |
| Giant-step parameter `m` | Maintained implementation uses `m = ceil(sqrt(M))`, where `M` is the effective order. | Inferred |
| Traversal sets | One baby-step list and two giant-step lists. | Inferred |
| Randomness | None in maintained implementation. | Inferred |
| Restart rule | None; one deterministic traversal until a collision or no solution. | Inferred |
| Storage model | Stores three traversal lists. | Inferred |

## 10. Las Vegas method

| Parameter / behaviour | Current maintained implementation | Historical confidence |
|---|---|---|
| Degree parameter | Maintained implementation sets `n' = log2(M)`, where `M` is the effective order. | Inferred |
| Matrix size parameter | `l = 3 n'`, adjusted downward if `l + 1 >= M`. | Inferred |
| Matrix field | Matrix entries reduced modulo the finite-field modulus. | Inferred |
| Random point coefficients | Random coefficients modulo the effective order. | Inferred |
| Kernel routine | Row reduction and left-kernel basis search. | Inferred |
| Attempt limit | Maintained implementation has `maxNumberOfAttepts = 10`; actual loop condition should be checked when interpreting exact attempt count. | Inferred / current-code note |
| Random seeds | Not recorded. | Not recorded |
| Parameter sweep | Not documented. | Not documented |

## 11. Performance measurement configuration

| Metric | Current maintained implementation / manuscript description | Historical confidence |
|---|---|---|
| Runtime | Measured using C# `Stopwatch`. | Confirmed by manuscript / maintained implementation |
| Memory metric | Maintained implementation records `GC.GetAllocatedBytesForCurrentThread()` difference. | Inferred |
| Warm-up | First 5% of rows treated as heating / warm-up. | Confirmed |
| Accuracy / completion metric | Stored as Boolean correctness per row, then aggregated as a percentage. | Inferred |
| Iteration count | Taken from the selected base solver inside Pohlig-Hellman. | Inferred |
| Result file schema | Maintained implementation writes: time, memory, correctness, iterations, negation-map flag, extended-negation-map flag. | Inferred |
| Exceptions | Maintained implementation catches exceptions and records failed results implicitly. | Current-code note |
| Worker count | Maintained implementation uses `Environment.ProcessorCount - 5`; manuscript states 11 threads. | Needs manuscript alignment |


## 12. Information that should not be claimed without additional evidence

Do not claim the following unless supporting artefacts are added:

1. exact bit-for-bit reproduction of the historical benchmark runs;
2. recorded historical random seeds;
3. exhaustive parameter optimisation;
4. systematic parameter-sensitivity analysis of Kangaroo or Las Vegas;
5. current `master` as the exact source snapshot used for every historical measurement;
6. standalone comparison of the five base solvers on prime-order production-grade ECC subgroups;
7. security relevance of the 25–30 bit benchmark sizes beyond controlled toy-scale evaluation.
