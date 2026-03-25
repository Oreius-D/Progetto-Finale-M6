# Progetto-Finale-M6
Progetto Finale Modulo M6

# Changelog

23/03/2026
- Introduzione Notazione [Obsolete]. Questa sta ad indicare precedenti cartelle che non vengono più utilizzati, l'intenzione è di rimuoverli a fine refactor, ma nel caso qualcuno dovesse sfuggire, fare riferimento a questa definizione

- Introduzione della Pixel Perfect Camera 2D e cinemachine al fine di garantire maggiore fluidità è risolvere la comparsa di quelle linee blu durante il gioco

23/03/2026
- Introduzione Notazione [Old]. Questa sta ad indicare precedenti script che non vengono più utilizzati e sono stati rinominati per non interferire con in nuovi riferimenti, l'intenzione è di rimuoverli a fine refactor, ma nel caso qualcuno dovesse sfuggire, fare riferimento a questa definizione.

-  Aggiunta scripts "Core" al fine di centralizzare meccaniche tra le cui:
  - Sound FX Manager, gestisce la riproduzione dei suoni tramite singleton che referenzia l' AudioPool.
  - Singleton, classe template per tutti i singleton del gioco.
  - Game Flow Manager, classe per la gestione del flusso del gioco tramite singleton.
  - Game Events, classe statica che contiene tutti gli eventi del gioco.
  - Audio Pool, classe per pooling degli audio, così da non creare e distruggere sempre tutti i suoni.
  - Interfacce IDamageable e IInteractable, per gestire l'infliggere il danno e interagire con gli oggetti in scena

-  Aggiunta scripts "Common Components" per gestire le componenti comuni tra i prefab:
  - Health, classe per gestire la logica di vita, danno, cura e morte.

-  Aggiunta scripts "Player" al fine di centralizzare meccaniche tra le cui:
  - Player Animation, classe che gestisce l'animazione del player.
  - Player Health Bridge, classe che fa da ponte tra Health e player.
  - Player Input, classe che gestisce gli input del giocatore.
  - Player Motor 2D, classe che gestisce il movimento 2D del player.

-  Aggiunta scripts "Enemy" al fine di centralizzare meccaniche tra le cui:
  - Enemy Manager, classe che gestisce tutti i nemici.
  - Enemy Movement, classe che muove i nemici verso il player.
  - Enemy Animation, classe che gestisce l'animazione dei nemici

-  File [Obsolete] o rimossi alla data:
  - [Obsolete]
    - Cartella camera che contiene CameraFollow.
    - Cartella Controllers che contiene:
      - LifeController, vecchio gestore della vita dei nemici.
      - Old Player Controller, vecchio gestore del player (monolitico).
    - Cartella Sound che contiene:
      - Game Music Manager, vecchio gestore della Musica in background.
      - Old Sound FX Manager, vecchio gestore dei suoni, creava e distruggeva ad ogni colpo.
  - Player Health Bridge, classe che fa da ponte tra Health e player
