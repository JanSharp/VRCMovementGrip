
# Changelog

## [1.2.1] - 2025-03-25

### Changed

- Add note to ensure to adjust the rigid body ([`30c944b`](https://github.com/JanSharp/VRCMovementGrip/commit/30c944b420c46a50be765cbd5929ebe2b7db9cdc))
- Update license year, a bit late ([`5a47074`](https://github.com/JanSharp/VRCMovementGrip/commit/5a47074b6973fa205c316bc6caecb42483cc2a3f))

## [1.2.0] - 2025-03-25

### Changed

- **Breaking:** Make min max variable names less confusing, especially when coming from blender ([`91ca7d9`](https://github.com/JanSharp/VRCMovementGrip/commit/91ca7d95c8cef67361310b4b58d74deb78f22a87))
- Bump dependency on `com.jansharp.common` to `v1.0.0` for the singleton management. Should have been part of `v1.1.5` ([`9a6b3cf`](https://github.com/JanSharp/VRCMovementGrip/commit/9a6b3cfa38a42d4fa267cd943e18dc7b20afc12d))

### Added

- Add SetLocalPositionOfToMove api function ([`c81d038`](https://github.com/JanSharp/VRCMovementGrip/commit/c81d03855d3efa111a1e22931895017b674fa8a1))
- Add begin and end movement events ([`8e9ebe0`](https://github.com/JanSharp/VRCMovementGrip/commit/8e9ebe0f845d1c3d67dddf9776172c80a77dae6a), [`626faa3`](https://github.com/JanSharp/VRCMovementGrip/commit/626faa357e8832296f751789f9d1632c3d7e8d80))

## [1.1.6] - 2025-03-25

_This should have been part of the previous release._

### Removed

- Remove code that is now handled by singleton editor scripting ([`c332e1d`](https://github.com/JanSharp/VRCMovementGrip/commit/c332e1de79c6166c5790b24d47fcb553384a285b))

## [1.1.5] - 2025-03-25

### Added

- Add singleton reference for UpdateManager to auto instantiate the prefab ([`90c6a0d`](https://github.com/JanSharp/VRCMovementGrip/commit/90c6a0d72da5b814e745872618a9912a037e80f8))

### Fixed

- Fix title in the readme ([`f0ade3b`](https://github.com/JanSharp/VRCMovementGrip/commit/f0ade3be057c733870b34258dd26fa3a395c06f2))

## [1.1.4] - 2023-10-12

### Changed

- Update vrc worlds dependency to 3.4.x ([`cf038c4`](https://github.com/JanSharp/VRCMovementGrip/commit/cf038c4a56d0317b66fc1cbf6e691a71d54d8eee))

### Removed

- Remove udonsharp dependency as it has been merged into worlds ([`cf038c4`](https://github.com/JanSharp/VRCMovementGrip/commit/cf038c4a56d0317b66fc1cbf6e691a71d54d8eee))

## [1.1.3] - 2023-08-05

### Changed

- Change LICENSE.txt to LICENSE.md so Unity sees it in the package manager window ([`99ea239`](https://github.com/JanSharp/VRCMovementGrip/commit/99ea239ec97ae27dcdf6d535d8373f4fa6fa3253))

### Added

- Add vpm dependency on `com.vrchat.worlds` for clarity ([`0522bf9`](https://github.com/JanSharp/VRCMovementGrip/commit/0522bf9c745145cc68ada36b0ac0c770fc81434a))

### Fixed

- Fix build error on publish ([`7dbdc95`](https://github.com/JanSharp/VRCMovementGrip/commit/7dbdc956872df558b3bd3d2017221ce8d5e449b0))

## [1.1.2] - 2023-07-23

### Added

- Add note about VRC Pickup orientation in readme ([`796e3d4`](https://github.com/JanSharp/VRCMovementGrip/commit/796e3d42d8872a4f349be6979837c900add7a066))

## [1.1.1] - 2023-07-17

_First version of this package that is in the VCC listing._

### Changed

- **Breaking:** Separate Movement Grip into its own repo ([`0dc30c7`](https://github.com/JanSharp/VRCMovementGrip/commit/0dc30c77906e9b66ee903a2bafe7862a27bc3732), [`7c93c50`](https://github.com/JanSharp/VRCMovementGrip/commit/7c93c5024bf6cd02b5e2ee311a6446ef041fd229))
- **Breaking:** Update OnBuildUtil and other general editor scripting, use SerializedObjects ([`de04745`](https://github.com/JanSharp/VRCMovementGrip/commit/de04745880f0ea37345b5fd4e54de94fe7f05368), [`ee4ffb5`](https://github.com/JanSharp/VRCMovementGrip/commit/ee4ffb5ffe6218097cd01b94becc93bafb6ad2ca))

### Added

- Add readme with listing link, features and ideas ([`7ca848d`](https://github.com/JanSharp/VRCMovementGrip/commit/7ca848d23bc00151ad1fb190a63b8b6fcca4bd8a))

## [1.1.0] - 2023-06-11

### Changed

- **Breaking:** Remove and change use of deprecated UdonSharp editor functions ([`8d198de`](https://github.com/JanSharp/VRCMovementGrip/commit/8d198de570fbbcadcefaab08146b802961d3fed3))
- **Breaking:** Use refactored OnBuildUtil ([`17dbab8`](https://github.com/JanSharp/VRCMovementGrip/commit/17dbab84b8bb6bad192d67607a5f45c8cd000356))
- Migrate to VRChat Creator Companion ([`9ae838c`](https://github.com/JanSharp/VRCMovementGrip/commit/9ae838cf1d6280c64c607559fb3ae9967b52bd99), [`78b73b6`](https://github.com/JanSharp/VRCMovementGrip/commit/78b73b6816612602b04daafeb4097351f087c01a), [`5507ce0`](https://github.com/JanSharp/VRCMovementGrip/commit/5507ce07957daf2ae50726105841d1430f5ff085))

## [1.0.2] - 2022-09-05

### Fixed

- Fix objects potentially "disappearing" if the instance owner has not moved them yet, for real this time ([`9773ce6`](https://github.com/JanSharp/VRCMovementGrip/commit/9773ce69737e1eb8d9c0a115b3a8179afcc8ad7b))

## [1.0.1] - 2022-08-21

### Fixed

- Fix objects potentially "disappearing" if the instance owner has not moved them yet ([`43ac7cf`](https://github.com/JanSharp/VRCMovementGrip/commit/43ac7cf16d1ce8807eca6c906209c1ec31524677))

## [1.0.0] - 2022-08-19

### Added

- Use hand position in VR for significantly more accurate movement, especially with big grips ([`b815938`](https://github.com/JanSharp/VRCMovementGrip/commit/b8159381c8f17bee8120250b8023f451fbcd0ebe))

### Fixed

- Fix non default transforms and differing parents for the grip and the moving object simply not working ([`46a5a47`](https://github.com/JanSharp/VRCMovementGrip/commit/46a5a47771e2f9d6ec397463920b506a4480fb42))

## [0.1.0] - 2022-08-03

### Added

- Add restrictions for each axis with max deviation ([`2e3b2cf`](https://github.com/JanSharp/VRCMovementGrip/commit/2e3b2cfdceb99e4ec9f4d5f766d456f2e59f44b5))
- Add syncing with interpolation ([`bc5fbaa`](https://github.com/JanSharp/VRCMovementGrip/commit/bc5fbaa546fb26282f3190f095a9a26858b54ffb), [`22b4550`](https://github.com/JanSharp/VRCMovementGrip/commit/22b455090db7e18b94d6bb3ad15cf6dae6f31a12), [`fe16657`](https://github.com/JanSharp/VRCMovementGrip/commit/fe16657dcab93e45422df4b9249c88f87b42a443))
- Drop the grip when multiple people pick it up at the same time ([`dce4ef4`](https://github.com/JanSharp/VRCMovementGrip/commit/dce4ef47a749ee4d2418cfbba921600f597765ef))

[1.2.1]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.2.1
[1.2.0]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.2.0
[1.1.6]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.6
[1.1.5]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.5
[1.1.4]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.4
[1.1.3]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.3
[1.1.2]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.2
[1.1.1]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/v1.1.1
[1.1.0]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/MovementGrip_v1.1.0
[1.0.2]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/MovementGrip_v1.0.2
[1.0.1]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/MovementGrip_v1.0.1
[1.0.0]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/MovementGrip_v1.0.0
[0.1.0]: https://github.com/JanSharp/VRCMovementGrip/releases/tag/MovementGrip_v0.1.0
