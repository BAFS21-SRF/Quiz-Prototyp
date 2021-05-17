<template>
  <div>
      <h2>Score Board</h2>
      <table v-if="games != null">
          <thead>
              <tr>
                  <th>Pos</th>
                  <th>Score</th>
                  <th>Name</th>
              </tr>
          </thead>
          <tbody>
              <tr v-for="(game, index) in games" :key="game.id">
                  <td>{{ index + 1 }}</td>
                  <td>{{ game.score }}</td>
                  <td>{{ game.guid }}</td>
              </tr>
          </tbody>
      </table>
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
th, td {
  text-align: left;
}
td {
   border-top: 1px solid black; 
}
table{
    width: 80%;
    border: 1px solid black;
}
</style>
