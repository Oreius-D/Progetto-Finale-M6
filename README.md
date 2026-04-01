# Progetto-Finale-M6
Progetto Finale Modulo M6

# Changelog

- Core
  - Aggiunta gestione degli eventi
  - Aggiunto gestore del flow di gioco
  - Aggiunto singleton per gestione del flow e audio manager
  - Aggiunto audio manager per suoni in gioco con audio pooling
  - Aggiunta contratti IDamageable e IInteractable per centralizzare la gestione dei danni e interazioni

- CommonComponents
  - Aggiunto componente centralizzato per la gestione della vita.
    
- UI
  - Aggiunto Menu iniziale
  - Aggiunto menu di pausa
  - Aggiunta schermata di vittoria (Si visualizza quando tutti i nemici vengono eliminati)
  - Aggiunta schermata di sconfitta (Si visualizza quando il player muore)
  - Aggiunta UI di gioco che mostra l'arma equipaggiata e i nemici sconfitti.

- Player
  - Rework componente gestione della animazioni
  - Separato l'input del player dal movimento effettivo
  - Aggiunta ponte per il componente condiviso della vita
  
 - Enemy
   - Rework componente gestione della animazioni
   - Aggiunto componente per contare i nemici presenti e uccisi per UI
   - Aggiunto componente per la gestione di tutti i nemici
   - Aggiunto script per movimento del singolo nemico

 - Weapon
   - L'arma raccolta non viene più sovrapposta al player, ma è mostrata nell'UI
   - Aggiunto WeaponController che si occupa di gestire il comportamento delle armi
   - Aggiunto WeaponPickup che si occupa solo di raccogliere l'arma ed equipaggiarla
   - Rework script per generazione proiettile
   - Aggiunto script WeaponDefinition usato per creare scriptable Objects che adesso definiscono le caratteristiche di un arma.
