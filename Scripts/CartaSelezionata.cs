/*using UnityEngine;

public class CartaSelezionata : MonoBehaviour
{
    // Titolo e testo della carta
    [HideInInspector] public string titolo;
    [HideInInspector] public string testo;

    // Funzione per aggiornare la carta in base al numero di pagina
    public void AggiornaCarta(int numeroPagina)
    {
        switch (numeroPagina)
        {
            case 7:
                titolo = "Protagonista";
                testo = "� ora di creare l�Eroe, l�Eroina o gli Eroi della tua storia.\n" +
                        "Per crearli puoi iniziare rispondendo a queste domande:\n" +
                        "Quanti anni ha? � pi� grande o pi� piccolo/a di te?\n" +
                        "Che aspetto ha? Come � fatto/a? Prova a descriverlo/a.\n" +
                        "Cos�ha di speciale? Ha qualche caratteristica straordinaria? Ha dei gusti o delle abitudini particolari?\n" +
                        "Di cosa ha paura? C�� qualcosa che lo/la terrorizza o che non pu� proprio sopportare?\n" +
                        "Cosa desidera? Cosa vuole di pi� al mondo?\n" +
                        "Come si chiama? Ha un nome?";
                break;

            case 8:
                titolo = "Antagonista";
                testo = "In tutte le storie c'� sempre qualcuno contro cui combattere!\n" +
                        "Per crearlo puoi lasciarti ispirare dalle seguenti domande:\n" +
                        "Quanti anni ha? Che aspetto ha? Prova a descriverlo/a.\n" +
                        "Cos�ha di speciale? Qualche caratteristica straordinaria? Gusti o abitudini particolari?\n" +
                        "Di cosa ha paura? C�� qualcosa che lo/la terrorizza o che non pu� proprio sopportare?\n" +
                        "Cosa desidera? Cosa vuole di pi� al mondo?\n" +
                        "Come si chiama? Ha un nome?";
                break;

            case 9:
                titolo = "Mondo";
                testo = "Il mondo � il luogo dove si svolge la tua avventura. Pu� essere un mondo di fiaba o una citt� del futuro, un impero lontano o il cortile di casa tua.\n" +
                        "Puoi immaginare ogni luogo e ogni tempo. Per crearlo puoi lasciarti ispirare dalle seguenti domande:\n" +
                        "� un mondo normale o ha qualcosa di straordinario?\n" +
                        "Dove si trova? Sulla terra, nello spazio, lontano o vicino?\n" +
                        "La storia � ambientata nel passato, nel presente o nel futuro?\n" +
                        "Com�� una giornata tipo? Che abitudini hanno gli abitanti?\n" +
                        "Che cosa fa una persona comune? Come si gioca, ci si diverte?\n" +
                        "Immagina i dettagli pi� piccoli della vita dei tuoi personaggi.";
                break;

            case 10:
                titolo = "Imprevisto";
                testo = "Non c�� nessuna storia senza un imprevisto. Tutto sembra normale ma a un certo punto succede qualcosa che mette in moto il racconto.\n" +
                        "Immagina cosa potrebbe succedere all�inizio della tua storia. L�imprevisto potrebbe essere l�arrivo o la partenza di qualcuno, l�acquisto o la perdita di qualcosa, o un cambiamento del mondo.\n" +
                        "Ecco delle domande per aiutarti a crearlo:\n" +
                        "Cosa succede all�inizio? � un avvenimento positivo o negativo?\n" +
                        "Qual � l�effetto sul protagonista e sul mondo? � colpa dell�antagonista? C�entra il/la cattivo/a? Come cambia la storia?\n" +
                        "Che cosa fa il/la protagonista per rispondere all�imprevisto?";
                break;
            case 13: // Mondo Ordinario
                titolo = "C'era una volta";
                testo = "Tutte le storie cominciano in una situazione di normalit�.\r\n� il momento in cui vediamo il la protagonista nel suo quotidiano, prima che l'avventura abbia inizio.\r\nIn Cenerentola vediamo che tutti trattano male la protagonista, in Pinocchio Geppetto costruisce la marionetta, nelle carte Struttura c � una Casetta che dorme di fianco al suo amato Albero. Qui puoi presentare il personaggio principale e il suo mondo.\r\nEsempio:\r\nC'era una volta Cappuccetto Rosso, una bambina che... (descriviamo l'eroina, le sue abitudini, dove vive ecc.).";
                break;
            case 14: // Chiamata all'azione
                titolo = "E poi un giorno";
                testo = "Che noia se non succede niente! Infatti a un certo punto capita qualcosa che mette in moto la storia. Magari ci hai gi� pensato con la carta Imprevisto: ricordi il coniglio bianco di Alice? Non solo: in harry Potter arrivano la civetta e il gigante hagrid per invitare harry alla scuola di magia: in Spider-Man il protagonista viene punto da un ragno radioattivo che gli d� i superpoteri: nelle carte\r\nStruttura arriva un Drago che ruba l'Albero della Casetta.\r\nInsomma, succede qualcosa!\r\nEsempio:\r\nE poi un giorno la Nonna si ammala e Cappuccetto\r\nRosso deve portarle da mangiare.";
                break;
            case 15: // La soglia
                titolo = "Inizia l'avventura";
                testo = "Dopo l'imprevisto, il/la protagonista deve scegliere cosa fare. Spesso la sua scelta � definitiva e d� inizio all'avventura vera e propria.\r\nNe Il signore degli anelli lo hobbit Frodo deve lasciare casa per distruggere un anello magico, in Kung fu panda il panda Po inizia lallenamento di arti marziali, nelle carte Struttura la Casetta parte all'inseguimento del Drago che ha rubato l'Albero.\r\nEsempio:\r\nCappuccetto Rosso parte in direzione del bosco per portare le focacce alla Nonna";
                break;
            case 16: // Prima prova
                titolo = "Per� ecco che";
                testo = "Che bello se il/la protagonista riuscisse subito a portare a termine la sua missione... ma va! Che noia! Mettiamogli un ostacolo da oltrepassare, sfidiamolo, rallentiamolo.\r\nNe Il re leone Simba visita il cimitero degli elefanti, in Biancaneve la protagonista deve sopravvivere nel bosco, nelle carte Struttura la Casetta deve superare una montagna e chiede aiuto a un'oca con la mongolfiera.\r\nPu� essere una prova, un nemico dasconfiggere, un sentimento da conoscere o superare. Spesso il/la protagonista riesce a superarlo, ma pu� capitare che fallisca, allora deve affrontarlo per una seconda o una terza volta.\r\nEsempio:\r\nCappuccetto Rosso sta andando verso casa della Nonna, per� ecco che incontra il Lupo che le chiede dove abita la Nonna. Lei non lo riconosce e gli d� l'informazione.";
                break;
            case 17: // Seconda prova
                titolo = "Per� ecco che";
                testo = "Gli ostacoli non arrivano mai da soli! Dopo il primo, ce n'� subito un altro. Cenerentola ad esempio non ha un abito per il ballo perch� le sue sorelle glielo hanno stracciato: in un racconto di paura, i protagonisti dopo aver sconfitto un lupo mannaro devono anche fuggire dalla mummia: nelle carte Struttura la Casetta si impiglia nei rovi e deve difendersi dai Corvi che la attaccano.\r\nEsempio:\r\nCappuccetto Rosso sta per riprendere la strada, per� ecco che il Lupo la tenta: lei ci casca e si attarda nel bosco per raccogliere un sacco di fiori, cos� il Lupo arriva prima dalla Nonna e la inghiotte. Cappuccetto Rosso non ha superato la prova.";
                break;
            case 18: // Lo scontro con l'antagonista
                titolo = "Lo scontro con l'antagonista";
                testo = "Prima o poi il la protagonista affronta la prova pi� difficile\r\nSe ha un nemico, � il momento dello scontro. In Frozen, Anna arriva finalmente al castello di Elsa e deve convincere la sorella a tornare a casa: Batman, dopo aver sconfitto tutti gli scagnozzi di Joker, affronta il vero cattivo della storia: nelle carte Struttura la Casetta combatte contro\r\nil Drago. Il nemico pu� anche essere una paura: per esempio nella storia di un bambino che ha paura del buio, la prova centrale pu� essere attraversare una stanza nera per salvare il suo gatto. Di solito � la cosa pi� difficile che si possa immaginare per il la protagonista.\r\nEsempio:\r\nCappuccetto Rosso arriva alla casa della Nonna e parla con il Lupo.";
                break;
            case 19: // Il risultato � che
                titolo = "Il risultato � che";
                testo = "La prova centrale ha sempre delle conseguenze. A volte l'antagonista viene sconfitto e il/la protagonista ottiene una ricompensa, come nelle storie di pirati in cui, sconfitto il calamaro gigante, si trova il grande tesoro; a volte vince l'antagonista e il/la protagonista perde qualcosa, per esempio Pinocchio va nel paese dei balocchi, non resiste alla tentazione e si trasforma in asino; nelle carte\r\nStruttura, la Casetta sconfigge il Drago e recupera l'Albero.\r\nEsempio:\r\nCappuccetto Rosso non riconosce il Lupo, fallisce la prova centrale e il risultato � che il Lupo la mangia.";
                break;
            case 20: // Per� capita ancora che
                titolo = "Per� capita ancora che";
                testo = "Se la storia finisse ora sarebbe incompleta. Dopo l'esito di solito c'� un nuovo imprevisto che cambia la situazione.\r\nIn Frozen Anna ha bisogno del bacio del fidanzato hans per salvarsi, ma lui si rifiuta: in Cenerentola dopo il ballo lei torna alla sua vita terribile, ma il principe bussa alla sua porta con la scarpetta: nelle carte Struttura arrivano i Corvi cattivi per� il Drago, pentito, per farsi perdonare dalla Casetta, sconfigge i nuovi nemici.\r\nEsempio:\r\nIl Lupo sta dormendo soddisfatto con in pancia Cappuccetto Rosso e la Nonna, per� capita ancora che arriva il Caccia-tore, capisce che qualcosa non va, scopre il Lupo addormentato e aiuta Cappuccetto.";
                break;
            case 21: // Conclusione
                titolo = "E alla fine";
                testo = "� l'ultimo atto della storia, il momento del \"tutti felici e contenti\". I conflitti sono risolti e s'instaura una nuova normalit� In genere il la protagonista decide se restare nel mondo dell'avventura oppure tornare a casa, cambiato/aharry Potter, alla fine dell'anno scolastico, torna dagli zii che lo trattano meglio perch� adesso lui � un mago: nelle carte Struttura la Casetta torna al suo posto con l'Albero e un nuovo amico: il Drago!\r\nEsempio:\r\nCappuccetto Rosso alla fine impara che deve obbedire alla\r\nMamma e non attardarsi nel bosco.";
                break;
            case 24:
                titolo = "A chi vuole bene il/la protagonista?";
                testo = "Il/la protagonista ha amici? Genitori? Una fidanzata o un fidanzato?\r\nTutti i personaggi hanno qualcuno a cui vogliono bene! In Frozen per esempio Anna vuole bene a sua sorella, Aladdin si innamora di Jasmine.\r\nEsempio:\r\nCappuccetto Rosso ha la Mamma e la Nonna.";
                break;
            case 25:
                titolo = "I personaggi hanno abbastanza aiuto?";
                testo = "hai gi� aggiunto personaggi che diano una mano? Per loro potrebbe essere troppo difficile cavarsela da soli.\r\nCosa avrebbe fatto Cenerentola senza l'aiuto della Fata che fa comparire un abito e la carrozza? Anche i supereroi pi� forti hanno degli alleati: Batman ha Robin e il suo maggiordomo.\r\nIl protagonista incontra sempre un personaggio che lo aiuta: Pinocchio ha il Grillo e la Fata Turchina, harry\r\nPotter conosce hermione e Ron. Con Cappuccetto Rosso bisogna aspettare il Cacciatore, ma senza di lui non ce l'avrebbe fatta.\r\nOltre ai protagonisti, anche gli antagonisti hanno spesso degli aiutanti: pensa ai complici dell'assassino in un giallo, o agli scagnozzi del supercattivo di Batman.";
                break;
            case 26:
                titolo = "Perch� l�antagonista si comporta cos�?";
                testo = "Perch� il cattivo � cattivo? � simile al protagonista?\r\nA volte l'antagonista non � cos� cattivo/a, magari � semplicemente incompreso. In Frozen Elsa sembra cattiva ma compie azioni malvagie solo perch� non riesce a controllare i propri\r\npoteri, e non sa come proteggere s� stessa e gli altri. Molti cattivi hanno una storia interessante, come Joker in\r\nBatman.\r\nA volte qualcuno sembra cattivo ma non lo �.\r\nPensa a Shrek:\r\n� un orco, se non lo conoscessimo ci farebbe paura!\r\nGuarda anche la carta Antagonista: secondo te perch� il Drago � cattivo? Cosa accadrebbe se lui e il protagonista si conoscessero meglio?\r\nIl Lupo di Cappuccetto Rosso sceglie di essere cattivo?";
                break;
            case 27:
                titolo = "Troppo facile?";
                testo = "Complica la vita ai tuoi personaggi. Rendi gli ostacoli ancora pi� difficili! Pensa a quante prove deve affrontare\r\nFrodo ne Il signore degli anelli.\r\nAnche i pi� grandi supereroi hanno dei momenti di difficolt�. Pi� grandi sono gli ostacoli da affrontare, migliore sar� la storia. Ti piacerebbe se i pirati trovassero subito il tesoro, senza naufragi, combattimenti e mostri marini?\r\nSe Cappuccetto Rosso avesse riconosciuto subito il\r\nLupo non sarebbe stato altrettanto emozionante.";
                break;
            case 28:
                titolo = "C�� gi� un colpo di scena?";
                testo = "Tutti pensano che succeda una cosa e invece... sorprendili!\r\nInserire un elemento inaspettato rende le storie avvincenti.\r\nSe qualcuno non fosse quello che sembra, come Hans in Frozen? Se si scoprisse qualcosa di molto importante solo a met� o alla fine della storia?\r\nIn un giallo spesso l'assassino � la persona meno sospetta.\r\nChe sorpresa scoprire che hans di Frozen non vuole\r\nbaciare Anna!";
                break;
            case 29:
                titolo = "Qual � il passato dei personaggi?"; ;
                testo = "E successo qualcosa prima? hai pensato a come raccontarlo?\r\nTutti i protagonisti hanno un passato, anche se non lo raccontiamo. Le loro esperienze precedenti possono determinare le loro azioni. Un pirata potrebbe essere schiavo di una maledizione passata, un ladro come Robin\r\nNood potrebbe rubare per vendicare un affronto.\r\nPensa anche a harry Potter: la sua cicatrice � il segno dello scontro che ha avuto con Voldemort nella culla.\r\nSappiamo che Cappuccetto Rosso � molto affezionata alla\r\nNonna e un po' disobbediente. E il Lupo che storia ha?\r\nE il Cacciatore?\r\nInoltre, anche i protagonisti secondari hanno un passato.\r\nPossiamo immaginarlo, e magari diventer� la nostra prossima storia.\r\nE che dire dellantagonista? Forse ce l'ha cos� tanto con illa protagonista proprio per qualcosa che ha vissuto.";
                break;
            case 30:
                titolo = "Chi racconta la storia?";
                testo = "E se la storia fosse raccontata dal/dalla protagonista? Al presente o al passato?\r\nC'� sempre qualcuno che racconta la storia. Pu� essere un protagonista, un testimone, o qualcuno che l'ha sentita da altri. Ne La storia infinita per esempio la storia del mondo fantastico in cui vive Atreyu ci viene raccontata dal protagonista. In un giallo spesso � l'investigatore a\r\nraccontare gli avvenimenti.\r\nInoltre la storia pu� essere raccontata come se fosse gi� successa, o come se stesse avvenendo ora.\r\nNel caso di Cappuccetto Rosso la racconta qualcuno di esterno, che non ha preso parte agli eventi, e la racconta al passato. Pensa se la raccontasse la Nonna, invece.";
                break;
            case 31:
                titolo = "C�� un momento di disperazione?";
                testo = "Hai pensato di far vivere al/alla protagonista un momento di disperazione? Ad esempio quando le sorelle le strappano l'abito Cenerentola � disperata, o quando\r\nPinocchio si trasforma in asino e pensa di non poter tornare mai pi� a casa.\r\nNoi facciamo cascare i personaggi perch� quando si rialzano sono pi� forti e profondi: e per emozionare ancora di pi� chi ci ascolta.\r\nIl momento in cui Cappuccetto Rosso viene inghiottita e sembra che non ci sia pi� speranza � il momento di disperazione della storia.";
                break;
            case 32:
                titolo = "Il/la protagonista ha un difetto?";
                testo = "Il/la protagonista ha difetti o caratteristiche negative?\r\nNessuno � perfetto.\r\nQual � il segreto del/della protagonista?\r\nPo di Kung fu panda � pigro, il Brutto Anatroccolo � diverso dai suoi fratelli, Cappuccetto Rosso disobbedisce alla Mamma e si ferma a cogliere fiori.\r\nI difetti ci fanno affezionare ai protagonisti, e a volte sono proprio le loro debolezze ad aiutarli a superare le prove: pensate all'incoscienza di Harry Potter.";
                break;
            case 33:
                titolo = "Cosa Impara il/la protagonista?";
                testo = "Com'� cambiato/a alla fine della storia?\r\nOgni esperienza cambia il protagonista e lo fa crescere.\r\nCenerentola impara che deve lottare per i suoi desideri, Pinocchio che deve essere sincero con chi gli vuole bene.\r\nSicuramente Cappuccetto Rosso non si fider� pi� delle apparenze la prossima volta.";
                break;

            default:
                titolo = "Errore";
                testo = "Numero pagina non valido!";
                break;
        }
    }
}*/



using UnityEngine;

public class CartaSelezionata : MonoBehaviour
{
    // Titolo e testo della carta
    [HideInInspector] public string titolo;
    [HideInInspector] public string testo;

    // Funzione per aggiornare la carta in base al numero di pagina
    public void AggiornaCarta(int numeroPagina)
    {
        switch (numeroPagina)
        {
            case 7:
                titolo = "Protagonista";
                testo = "� ora di creare l�Eroe, l�Eroina o gli Eroi della tua storia.\n" +
                        "Fai una breve descrizione di quello/a che pensi possa essere il/la protagonista, indicando il nome e magari la sua et�, caratteristiche fisiche ecc...";
                break;

            case 8:
                titolo = "Antagonista";
                testo = "In tutte le storie c'� sempre qualcuno contro cui combattere!\n" +
                        "Fai una breve descrizione di quello/a che pensi possa essere il/la antagonista, indicando il nome e magari la sua et�, caratteristiche fisiche ecc...";
                break;

            case 9:
                titolo = "Mondo";
                testo = "Il mondo � il luogo dove si svolge la tua avventura. Pu� essere un mondo di fiaba o una citt� del futuro, un impero lontano o il cortile di casa tua.\n" +
                        "Puoi immaginare ogni luogo e ogni tempo. Genera un mondo in cui potrebbe ambientarsi la storia ";
                break;

            case 10:
                titolo = "Imprevisto";
                testo = "Non c�� nessuna storia senza un imprevisto. Tutto sembra normale ma a un certo punto succede qualcosa che mette in moto il racconto.\n" +
                        "Genera quello che potrebbe essere un imprevisto per la storia";
                break;

            case 13: // Mondo Ordinario
                titolo = "C'era una volta";
                testo = "Tutte le storie cominciano in una situazione di normalit�.\r\n� il momento in cui vediamo il la protagonista nel suo quotidiano, prima che l'avventura abbia inizio.\r\nIn Cenerentola vediamo che tutti trattano male la protagonista, in Pinocchio Geppetto costruisce la marionetta, nelle carte Struttura c � una Casetta che dorme di fianco al suo amato Albero. Qui puoi presentare il personaggio principale e il suo mondo.\r\nEsempio:\r\nC'era una volta Cappuccetto Rosso, una bambina che... (descriviamo l'eroina, le sue abitudini, dove vive ecc.).";
                break;
            case 14: // Chiamata all'azione
                titolo = "E poi un giorno";
                testo = "Che noia se non succede niente! Infatti a un certo punto capita qualcosa che mette in moto la storia. Magari ci hai gi� pensato con la carta Imprevisto: ricordi il coniglio bianco di Alice? Non solo: in harry Potter arrivano la civetta e il gigante hagrid per invitare harry alla scuola di magia: in Spider-Man il protagonista viene punto da un ragno radioattivo che gli d� i superpoteri: nelle carte\r\nStruttura arriva un Drago che ruba l'Albero della Casetta.\r\nInsomma, succede qualcosa!\r\nEsempio:\r\nE poi un giorno la Nonna si ammala e Cappuccetto\r\nRosso deve portarle da mangiare.";
                break;
            case 15: // La soglia
                titolo = "Inizia l'avventura";
                testo = "Dopo l'imprevisto, il/la protagonista deve scegliere cosa fare. Spesso la sua scelta � definitiva e d� inizio all'avventura vera e propria.\r\nNe Il signore degli anelli lo hobbit Frodo deve lasciare casa per distruggere un anello magico, in Kung fu panda il panda Po inizia lallenamento di arti marziali, nelle carte Struttura la Casetta parte all'inseguimento del Drago che ha rubato l'Albero.\r\nEsempio:\r\nCappuccetto Rosso parte in direzione del bosco per portare le focacce alla Nonna";
                break;
            case 16: // Prima prova
                titolo = "Per� ecco che";
                testo = "Che bello se il/la protagonista riuscisse subito a portare a termine la sua missione... ma va! Che noia! Mettiamogli un ostacolo da oltrepassare, sfidiamolo, rallentiamolo.\r\nNe Il re leone Simba visita il cimitero degli elefanti, in Biancaneve la protagonista deve sopravvivere nel bosco, nelle carte Struttura la Casetta deve superare una montagna e chiede aiuto a un'oca con la mongolfiera.\r\nPu� essere una prova, un nemico dasconfiggere, un sentimento da conoscere o superare. Spesso il/la protagonista riesce a superarlo, ma pu� capitare che fallisca, allora deve affrontarlo per una seconda o una terza volta.\r\nEsempio:\r\nCappuccetto Rosso sta andando verso casa della Nonna, per� ecco che incontra il Lupo che le chiede dove abita la Nonna. Lei non lo riconosce e gli d� l'informazione.";
                break;
            case 17: // Seconda prova
                titolo = "Per� ecco che";
                testo = "Gli ostacoli non arrivano mai da soli! Dopo il primo, ce n'� subito un altro. Cenerentola ad esempio non ha un abito per il ballo perch� le sue sorelle glielo hanno stracciato: in un racconto di paura, i protagonisti dopo aver sconfitto un lupo mannaro devono anche fuggire dalla mummia: nelle carte Struttura la Casetta si impiglia nei rovi e deve difendersi dai Corvi che la attaccano.\r\nEsempio:\r\nCappuccetto Rosso sta per riprendere la strada, per� ecco che il Lupo la tenta: lei ci casca e si attarda nel bosco per raccogliere un sacco di fiori, cos� il Lupo arriva prima dalla Nonna e la inghiotte. Cappuccetto Rosso non ha superato la prova.";
                break;
            case 18: // Lo scontro con l'antagonista
                titolo = "Lo scontro con l'antagonista";
                testo = "Prima o poi il la protagonista affronta la prova pi� difficile\r\nSe ha un nemico, � il momento dello scontro. In Frozen, Anna arriva finalmente al castello di Elsa e deve convincere la sorella a tornare a casa: Batman, dopo aver sconfitto tutti gli scagnozzi di Joker, affronta il vero cattivo della storia: nelle carte Struttura la Casetta combatte contro\r\nil Drago. Il nemico pu� anche essere una paura: per esempio nella storia di un bambino che ha paura del buio, la prova centrale pu� essere attraversare una stanza nera per salvare il suo gatto. Di solito � la cosa pi� difficile che si possa immaginare per il la protagonista.\r\nEsempio:\r\nCappuccetto Rosso arriva alla casa della Nonna e parla con il Lupo.";
                break;
            case 19: // Il risultato � che
                titolo = "Il risultato � che";
                testo = "La prova centrale ha sempre delle conseguenze. A volte l'antagonista viene sconfitto e il/la protagonista ottiene una ricompensa, come nelle storie di pirati in cui, sconfitto il calamaro gigante, si trova il grande tesoro; a volte vince l'antagonista e il/la protagonista perde qualcosa, per esempio Pinocchio va nel paese dei balocchi, non resiste alla tentazione e si trasforma in asino; nelle carte\r\nStruttura, la Casetta sconfigge il Drago e recupera l'Albero.\r\nEsempio:\r\nCappuccetto Rosso non riconosce il Lupo, fallisce la prova centrale e il risultato � che il Lupo la mangia.";
                break;
            case 20: // Per� capita ancora che
                titolo = "Per� capita ancora che";
                testo = "Se la storia finisse ora sarebbe incompleta. Dopo l'esito di solito c'� un nuovo imprevisto che cambia la situazione.\r\nIn Frozen Anna ha bisogno del bacio del fidanzato hans per salvarsi, ma lui si rifiuta: in Cenerentola dopo il ballo lei torna alla sua vita terribile, ma il principe bussa alla sua porta con la scarpetta: nelle carte Struttura arrivano i Corvi cattivi per� il Drago, pentito, per farsi perdonare dalla Casetta, sconfigge i nuovi nemici.\r\nEsempio:\r\nIl Lupo sta dormendo soddisfatto con in pancia Cappuccetto Rosso e la Nonna, per� capita ancora che arriva il Caccia-tore, capisce che qualcosa non va, scopre il Lupo addormentato e aiuta Cappuccetto.";
                break;
            case 21: // Conclusione
                titolo = "E alla fine";
                testo = "� l'ultimo atto della storia, il momento del \"tutti felici e contenti\". I conflitti sono risolti e s'instaura una nuova normalit� In genere il la protagonista decide se restare nel mondo dell'avventura oppure tornare a casa, cambiato/aharry Potter, alla fine dell'anno scolastico, torna dagli zii che lo trattano meglio perch� adesso lui � un mago: nelle carte Struttura la Casetta torna al suo posto con l'Albero e un nuovo amico: il Drago!\r\nEsempio:\r\nCappuccetto Rosso alla fine impara che deve obbedire alla\r\nMamma e non attardarsi nel bosco.";
                break;
            case 24:
                titolo = "A chi vuole bene il/la protagonista?";
                testo = "Il/la protagonista ha amici? Genitori? Una fidanzata o un fidanzato?\r\nTutti i personaggi hanno qualcuno a cui vogliono bene! In Frozen per esempio Anna vuole bene a sua sorella, Aladdin si innamora di Jasmine.\r\nEsempio:\r\nCappuccetto Rosso ha la Mamma e la Nonna.";
                break;
            case 25:
                titolo = "I personaggi hanno abbastanza aiuto?";
                testo = "hai gi� aggiunto personaggi che diano una mano? Per loro potrebbe essere troppo difficile cavarsela da soli.\r\nCosa avrebbe fatto Cenerentola senza l'aiuto della Fata che fa comparire un abito e la carrozza? Anche i supereroi pi� forti hanno degli alleati: Batman ha Robin e il suo maggiordomo.\r\nIl protagonista incontra sempre un personaggio che lo aiuta: Pinocchio ha il Grillo e la Fata Turchina, harry\r\nPotter conosce hermione e Ron. Con Cappuccetto Rosso bisogna aspettare il Cacciatore, ma senza di lui non ce l'avrebbe fatta.\r\nOltre ai protagonisti, anche gli antagonisti hanno spesso degli aiutanti: pensa ai complici dell'assassino in un giallo, o agli scagnozzi del supercattivo di Batman.";
                break;
            case 26:
                titolo = "Perch� l�antagonista si comporta cos�?";
                testo = "Perch� il cattivo � cattivo? � simile al protagonista?\r\nA volte l'antagonista non � cos� cattivo/a, magari � semplicemente incompreso. In Frozen Elsa sembra cattiva ma compie azioni malvagie solo perch� non riesce a controllare i propri\r\npoteri, e non sa come proteggere s� stessa e gli altri. Molti cattivi hanno una storia interessante, come Joker in\r\nBatman.\r\nA volte qualcuno sembra cattivo ma non lo �.\r\nPensa a Shrek:\r\n� un orco, se non lo conoscessimo ci farebbe paura!\r\nGuarda anche la carta Antagonista: secondo te perch� il Drago � cattivo? Cosa accadrebbe se lui e il protagonista si conoscessero meglio?\r\nIl Lupo di Cappuccetto Rosso sceglie di essere cattivo?";
                break;
            case 27:
                titolo = "Troppo facile?";
                testo = "Complica la vita ai tuoi personaggi. Rendi gli ostacoli ancora pi� difficili! Pensa a quante prove deve affrontare\r\nFrodo ne Il signore degli anelli.\r\nAnche i pi� grandi supereroi hanno dei momenti di difficolt�. Pi� grandi sono gli ostacoli da affrontare, migliore sar� la storia. Ti piacerebbe se i pirati trovassero subito il tesoro, senza naufragi, combattimenti e mostri marini?\r\nSe Cappuccetto Rosso avesse riconosciuto subito il\r\nLupo non sarebbe stato altrettanto emozionante.";
                break;
            case 28:
                titolo = "C�� gi� un colpo di scena?";
                testo = "Tutti pensano che succeda una cosa e invece... sorprendili!\r\nInserire un elemento inaspettato rende le storie avvincenti.\r\nSe qualcuno non fosse quello che sembra, come Hans in Frozen? Se si scoprisse qualcosa di molto importante solo a met� o alla fine della storia?\r\nIn un giallo spesso l'assassino � la persona meno sospetta.\r\nChe sorpresa scoprire che hans di Frozen non vuole\r\nbaciare Anna!";
                break;
            case 29:
                titolo = "Qual � il passato dei personaggi?"; ;
                testo = "E successo qualcosa prima? hai pensato a come raccontarlo?\r\nTutti i protagonisti hanno un passato, anche se non lo raccontiamo. Le loro esperienze precedenti possono determinare le loro azioni. Un pirata potrebbe essere schiavo di una maledizione passata, un ladro come Robin\r\nNood potrebbe rubare per vendicare un affronto.\r\nPensa anche a harry Potter: la sua cicatrice � il segno dello scontro che ha avuto con Voldemort nella culla.\r\nSappiamo che Cappuccetto Rosso � molto affezionata alla\r\nNonna e un po' disobbediente. E il Lupo che storia ha?\r\nE il Cacciatore?\r\nInoltre, anche i protagonisti secondari hanno un passato.\r\nPossiamo immaginarlo, e magari diventer� la nostra prossima storia.\r\nE che dire dellantagonista? Forse ce l'ha cos� tanto con illa protagonista proprio per qualcosa che ha vissuto.";
                break;
            case 30:
                titolo = "Chi racconta la storia?";
                testo = "E se la storia fosse raccontata dal/dalla protagonista? Al presente o al passato?\r\nC'� sempre qualcuno che racconta la storia. Pu� essere un protagonista, un testimone, o qualcuno che l'ha sentita da altri. Ne La storia infinita per esempio la storia del mondo fantastico in cui vive Atreyu ci viene raccontata dal protagonista. In un giallo spesso � l'investigatore a\r\nraccontare gli avvenimenti.\r\nInoltre la storia pu� essere raccontata come se fosse gi� successa, o come se stesse avvenendo ora.\r\nNel caso di Cappuccetto Rosso la racconta qualcuno di esterno, che non ha preso parte agli eventi, e la racconta al passato. Pensa se la raccontasse la Nonna, invece.";
                break;
            case 31:
                titolo = "C�� un momento di disperazione?";
                testo = "Hai pensato di far vivere al/alla protagonista un momento di disperazione? Ad esempio quando le sorelle le strappano l'abito Cenerentola � disperata, o quando\r\nPinocchio si trasforma in asino e pensa di non poter tornare mai pi� a casa.\r\nNoi facciamo cascare i personaggi perch� quando si rialzano sono pi� forti e profondi: e per emozionare ancora di pi� chi ci ascolta.\r\nIl momento in cui Cappuccetto Rosso viene inghiottita e sembra che non ci sia pi� speranza � il momento di disperazione della storia.";
                break;
            case 32:
                titolo = "Il/la protagonista ha un difetto?";
                testo = "Il/la protagonista ha difetti o caratteristiche negative?\r\nNessuno � perfetto.\r\nQual � il segreto del/della protagonista?\r\nPo di Kung fu panda � pigro, il Brutto Anatroccolo � diverso dai suoi fratelli, Cappuccetto Rosso disobbedisce alla Mamma e si ferma a cogliere fiori.\r\nI difetti ci fanno affezionare ai protagonisti, e a volte sono proprio le loro debolezze ad aiutarli a superare le prove: pensate all'incoscienza di Harry Potter.";
                break;
            case 33:
                titolo = "Cosa Impara il/la protagonista?";
                testo = "Com'� cambiato/a alla fine della storia?\r\nOgni esperienza cambia il protagonista e lo fa crescere.\r\nCenerentola impara che deve lottare per i suoi desideri, Pinocchio che deve essere sincero con chi gli vuole bene.\r\nSicuramente Cappuccetto Rosso non si fider� pi� delle apparenze la prossima volta.";
                break;

            default:
                titolo = "Errore";
                testo = "Numero pagina non valido!";
                break;
        }
    }
}



