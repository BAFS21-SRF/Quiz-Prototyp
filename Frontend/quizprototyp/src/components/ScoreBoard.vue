<template>
   <div>
      <v-card>
         <v-card-title>Score Board</v-card-title>
         <v-card-text>
            <v-simple-table v-if="games != null">
               <template v-slot:default>
                  <thead>
                     <tr>
                        <th>Pos</th>
                        <th>Score</th>
                        <th>Player GUID</th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr v-for="(game, index) in games" :key="game.id">
                        <td>{{ index + 1 }}</td>
                        <td>{{ game.score }}</td>
                        <td>{{ game.guid }}</td>
                     </tr>
                  </tbody>
               </template>
            </v-simple-table>
         </v-card-text>
      </v-card>
   </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class ScoreBoard extends Vue {
    private games: any = null;

    private async mounted() {
        this.loadGames();

        setInterval(() => {
            this.loadGames();
        }, 5000);
    }

    private async loadGames() {
        const response = await axios.get('http://localhost:8888/api/game');
        this.games = response.data;
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
