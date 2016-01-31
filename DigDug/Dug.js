// JavaScript Document

var moveright = 0;
var moveleft = 0;
var moveup = 0;
var movedown = 0;
var score = 0;
var loadcount = 0;
var digint;
var enemyint;
var stateint;
var fallint;
var bombcnt = 2;
var is_bomb1 = 0;
var is_bomb2 = 0;
var level = 1;
var is_pause = 0;
var leftlim = [];
var rightlim = [];
var state = [];
var roc = [];
var dx = [];
var dy = [];

function Resetgame()
{
	for( var b1 = 0; b1 < 48; b1++)
	{
		for( var b2 = 0; b2 < 2; b2++)
		{
			var bluediv = document.getElementById(b1 + "blue" + b2);
			document.getElementById("Game").removeChild(bluediv);
		}
	}
	for(var g1 = 0; g1 < 48; g1++)
	{
		var grasdiv = document.getElementById("gras" + g1);
		document.getElementById("Game").removeChild(grasdiv);
	}
	for(var j = 0; j < 51; j++)
	{
		for(var i = 0; i < 48; i++)
		{
			var tilediv = document.getElementById(i + "tile" + j);
			if(tilediv != null)
				document.getElementById("Game").removeChild(tilediv);
		}
	}
	if(level == 1)
	{
		for(var i = 0; i < 3; i++)
		{
			var spidiv = document.getElementById("Spid" + i);
			if(spidiv != null)
				document.getElementById("Game").removeChild(spidiv);	
		}
		for(var j = 0; j< 2; j++)
		{
			var rockdiv = document.getElementById("Rock" + j);
			if(rockdiv != null)
				document.getElementById("Game").removeChild(rockdiv);
		}
	}
	var bombdiv = document.getElementById("Bomb");
	if(bombdiv != null)
		document.getElementById("Game").removeChild(bombdiv);
	var bombdiv2 = document.getElementById("Bomb1");
	if(bombdiv2 != null)
		document.getElementById("Game").removeChild(bombdiv2);
	var scorediv = document.getElementById("Score");
	document.getElementById("Game").removeChild(scorediv);
	clearInterval(digint);
	clearInterval(enemyint);
	clearInterval(stateint);
	clearInterval(fallint);
	bombcnt = 2;
	is_bomb1 = 0;
	is_bomb2 = 0;
	moveright = 0;
	moveleft = 0;
	moveup = 0;
	movedown = 0;
	level = 1;
	isbomb = 0;
	score = 0;
	Createlevel();
}

function Checkcol()
{
	var dugdiv = document.getElementById("Doug");
	for( var j = 0; j < 51; j++)
	{
		for( var i = 0; i < 48; i++)
		{
			var tilediv = document.getElementById(i + "tile" + j);
			if(tilediv != null)
			{
				if( (tilediv.offsetLeft >= dugdiv.offsetLeft) && (tilediv.offsetLeft < ( dugdiv.offsetLeft + dugdiv.clientWidth) ) && (tilediv.offsetTop >= dugdiv.offsetTop) && (tilediv.offsetTop < ( dugdiv.offsetTop + dugdiv.clientHeight) ) )
				{
					document.getElementById("Game").removeChild(tilediv);
				}
			}
		}
	}
	if(level == 1)
	{
		for(var i = 0; i < 3; i++)
		{
			var spidiv = document.getElementById("Spid" + i);
			if(spidiv != null)
			{
				if( ( (dugdiv.offsetTop < spidiv.offsetTop) && ( (dugdiv.offsetTop + dugdiv.clientHeight) >= spidiv.offsetTop) ) || ( ( (dugdiv.offsetTop + dugdiv.clientHeight) > (     spidiv.offsetTop + spidiv.clientHeight) ) && ( dugdiv.offsetTop <= (spidiv.offsetTop + spidiv.clientHeight) ) ) || ( (dugdiv.offsetTop >= spidiv.offsetTop) && ( (dugdiv.offsetTop + dugdiv.clientHeight) <= (spidiv.offsetTop + spidiv.clientHeight) ) ) )
				{
					if( ( (dugdiv.offsetLeft < spidiv.offsetLeft) && ( (dugdiv.offsetLeft + dugdiv.clientWidth) >= spidiv.offsetLeft) ) || ( ( (dugdiv.offsetLeft + dugdiv.clientWidth) > (spidiv.offsetLeft + spidiv.clientWidth) ) && ( dugdiv.offsetLeft <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) || ( (dugdiv.offsetLeft >= spidiv.offsetLeft) && ( (dugdiv.offsetLeft + dugdiv.clientWidth) <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) )
					{
						document.getElementById("Game").removeChild(dugdiv);
						var deathaudio = new Audio("sounds/DougDeath.wav");
						deathaudio.play();
						setTimeout(Resetgame, 1000);
					}
				}
			}
		}
	}
}

function Setstate()
{
	if(is_pause == 0)
	{
		if(level == 1)
		{
			for(var i = 0; i < 3; i++)
			{
				if(state[i] == 1)
				{
					var ran = Math.floor(Math.random() * 4);
					if(ran == 2)
					{
						state[i] = 2;					
					}
				}
			}
		}
	}
}

function Checkenemy()
{
	for(var j = 0; j < 5; j++)
	{
		var bombdiv = document.getElementById("Exp" + j);
		if(level == 1)
		{
			for(var i = 0; i < 3; i++)
			{
				var spidiv = document.getElementById("Spid" + i);
				if( (spidiv != null) && (bombdiv != null) )
				{
					if( ( (bombdiv.offsetTop < spidiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) >= spidiv.offsetTop) ) || ( ( (bombdiv.offsetTop + bombdiv.clientHeight) > (     spidiv.offsetTop + spidiv.clientHeight) ) && ( bombdiv.offsetTop <= (spidiv.offsetTop + spidiv.clientHeight) ) ) || ( (bombdiv.offsetTop >= spidiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) <= (spidiv.offsetTop + spidiv.clientHeight) ) ) )
					{
						if( ( (bombdiv.offsetLeft < spidiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) >= spidiv.offsetLeft) ) || ( ( (bombdiv.offsetLeft + bombdiv.clientWidth) > (spidiv.offsetLeft + spidiv.clientWidth) ) && ( bombdiv.offsetLeft <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) || ( (bombdiv.offsetLeft >= spidiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) )
						{
							document.getElementById("Game").removeChild(spidiv);
							score = score + 10;
							document.getElementById("Score").innerHTML = "Score : " + score;
							var enemyaudio = new Audio("sounds/EnemyDeath.wav");
							enemyaudio.play();
						}
					}
				}
			}
		}
		var dugdiv = document.getElementById("Doug");
		if( (dugdiv != null) && (bombdiv != null) )
		{
			if( ( (bombdiv.offsetTop < dugdiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) >= dugdiv.offsetTop) ) || ( ( (bombdiv.offsetTop + bombdiv.clientHeight) > (     dugdiv.offsetTop + dugdiv.clientHeight) ) && ( bombdiv.offsetTop <= (dugdiv.offsetTop + dugdiv.clientHeight) ) ) || ( (bombdiv.offsetTop >= dugdiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) <= (dugdiv.offsetTop + dugdiv.clientHeight) ) ) )
			{
				if( ( (bombdiv.offsetLeft < dugdiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) >= dugdiv.offsetLeft) ) || ( ( (bombdiv.offsetLeft + bombdiv.clientWidth) > (dugdiv.offsetLeft + dugdiv.clientWidth) ) && ( bombdiv.offsetLeft <= (dugdiv.offsetLeft + dugdiv.clientWidth) ) ) || ( (bombdiv.offsetLeft >= dugdiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) <= (dugdiv.offsetLeft + dugdiv.clientWidth) ) ) )
				{
					document.getElementById("Game").removeChild(dugdiv);
					var deathaudio = new Audio("sounds/DougDeath.wav");
					deathaudio.play();
					setTimeout(Resetgame, 1000);		
				}
			}
		}
		bombdiv = document.getElementById("Exp1" + j);
		if(level == 1)
		{
			for(var i = 0; i < 3; i++)
			{
				var spidiv = document.getElementById("Spid" + i);
				if( (spidiv != null) && (bombdiv != null) )
				{
					if( ( (bombdiv.offsetTop < spidiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) >= spidiv.offsetTop) ) || ( ( (bombdiv.offsetTop + bombdiv.clientHeight) > (     spidiv.offsetTop + spidiv.clientHeight) ) && ( bombdiv.offsetTop <= (spidiv.offsetTop + spidiv.clientHeight) ) ) || ( (bombdiv.offsetTop >= spidiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) <= (spidiv.offsetTop + spidiv.clientHeight) ) ) )
					{
						if( ( (bombdiv.offsetLeft < spidiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) >= spidiv.offsetLeft) ) || ( ( (bombdiv.offsetLeft + bombdiv.clientWidth) > (spidiv.offsetLeft + spidiv.clientWidth) ) && ( bombdiv.offsetLeft <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) || ( (bombdiv.offsetLeft >= spidiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) )
						{
							document.getElementById("Game").removeChild(spidiv);
							score = score + 10;
							document.getElementById("Score").innerHTML = "Score : " + score;
							var enemyaudio = new Audio("sounds/EnemyDeath.wav");
							enemyaudio.play();
						}
					}
				}
			}
		}
		var dugdiv = document.getElementById("Doug");
		if( (dugdiv != null) && (bombdiv != null) )
		{
			if( ( (bombdiv.offsetTop < dugdiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) >= dugdiv.offsetTop) ) || ( ( (bombdiv.offsetTop + bombdiv.clientHeight) > (     dugdiv.offsetTop + dugdiv.clientHeight) ) && ( bombdiv.offsetTop <= (dugdiv.offsetTop + dugdiv.clientHeight) ) ) || ( (bombdiv.offsetTop >= dugdiv.offsetTop) && ( (bombdiv.offsetTop + bombdiv.clientHeight) <= (dugdiv.offsetTop + dugdiv.clientHeight) ) ) )
			{
				if( ( (bombdiv.offsetLeft < dugdiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) >= dugdiv.offsetLeft) ) || ( ( (bombdiv.offsetLeft + bombdiv.clientWidth) > (dugdiv.offsetLeft + dugdiv.clientWidth) ) && ( bombdiv.offsetLeft <= (dugdiv.offsetLeft + dugdiv.clientWidth) ) ) || ( (bombdiv.offsetLeft >= dugdiv.offsetLeft) && ( (bombdiv.offsetLeft + bombdiv.clientWidth) <= (dugdiv.offsetLeft + dugdiv.clientWidth) ) ) )
				{
					document.getElementById("Game").removeChild(dugdiv);
					var deathaudio = new Audio("sounds/DougDeath.wav");
					deathaudio.play();
					setTimeout(Resetgame, 1000);		
				}
			}
		}
	}
}

function MoveEnemy()
{
	if(is_pause == 0)
	{
		if(level == 1)
		{
			for(var i = 0; i < 3; i++)
			{
				if(state[i] == 1)
				{
					var tempen = document.getElementById("Spid" + i);
					if(tempen != null)
					{
						var templ = tempen.offsetLeft;
						var tempt = tempen.offsetTop;
						if(templ <= leftlim[i])
						{
							dx[i] = dx[i] * (-1);
							document.getElementById("Spider" + i).style.top = "-82px";
							document.getElementById("Spider" + i).style.left = "-75px";	
						}
						else if( (templ + tempen.clientWidth) >= rightlim[i])
						{
							dx[i] = dx[i] * (-1);
							document.getElementById("Spider" + i).style.top = "-20px";
							document.getElementById("Spider" + i).style.left = "0px";		
						}
						if( (tempt <= leftlim[i]) || ( (tempt + tempen.clientWidth) >= rightlim[i]) )
							dy[i] = dy[i] * (-1);
						templ = templ - dx[i];
						tempt = tempt - dy[i];
						tempen.style.left = templ + "px";
						tempen.style.top = tempt + "px";
					}
				}
				else if(state[i] == 2)
				{
					var dugdiv = document.getElementById("Doug");
					var tempen = document.getElementById("Spid" + i);
					if( (tempen != null) && (dugdiv != null) )
					{
						var vecx = dugdiv.offsetLeft - tempen.offsetLeft;
						var vecy = dugdiv.offsetTop - tempen.offsetTop;
						var den = Math.sqrt( Math.pow(vecx,2) + Math.pow(vecy,2) )
						vecx = vecx / den;
						vecy = vecy / den;
						vecx = vecx * 6;
						vecy = vecy * 6;
						tempen.style.left = tempen.offsetLeft + vecx + "px";
						tempen.style.top = tempen.offsetTop + vecy + "px";
						document.getElementById("Spider" + i).style.top = "-20px";
						document.getElementById("Spider" + i).style.left = "-75px";
						if( (tempen.offsetLeft >= (dugdiv.offsetLeft - 100) ) && (tempen.offsetLeft <= (dugdiv.offsetLeft + dugdiv.clientWidth + 100) ) && (tempen.offsetTop >= (dugdiv.offsetTop - 100) ) && (tempen.offsetLeft <= (dugdiv.offsetTop + dugdiv.clientHeight + 100) ) )
						{
							var checkpos = 3;
							for( var j = 0; j < 51; j++)
							{
								for( var k = 0; k < 48; k++)
								{
									var tilediv = document.getElementById(k + "tile" + j);
									if(tilediv != null)
									{
										if( (tilediv.offsetLeft <= tempen.offsetLeft) && ( (tilediv.offsetLeft + tilediv.clientWidth) >= tempen.offsetLeft) && (tilediv.offsetTop <= tempen.offsetTop) && ( (tilediv.offsetTop + tilediv.clientHeight) >= tempen.offsetTop) )
											checkpos = checkpos - 1;
										if( (tilediv.offsetLeft <= (tempen.offsetLeft + 16) ) && ( (tilediv.offsetLeft + tilediv.clientWidth) >= (tempen.offsetLeft + 16) ) && (tilediv.offsetTop <= tempen.offsetTop) && ( (tilediv.offsetTop + tilediv.clientHeight) >= tempen.offsetTop))
											checkpos = checkpos - 1;
										if( (tilediv.offsetLeft <= (tempen.offsetLeft + 32) ) && ( (tilediv.offsetLeft + tilediv.clientWidth) >= (tempen.offsetLeft + 32) ) && (tilediv.offsetTop <= tempen.offsetTop) && ( (tilediv.offsetTop + tilediv.clientHeight) >= tempen.offsetTop))
											checkpos = checkpos - 1;
									}
								}
							}
							if(checkpos == 3)
								state[i] = 3;
						}
					}
				}
				else if(state[i] == 3)
				{
					var dugdiv = document.getElementById("Doug");
					var tempen = document.getElementById("Spid" + i);
					if( (tempen != null) && (dugdiv != null) )
					{
						var vecx = dugdiv.offsetLeft - tempen.offsetLeft;
						var vecy = dugdiv.offsetTop - tempen.offsetTop;
						var den = Math.sqrt( Math.pow(vecx,2) + Math.pow(vecy,2) )
						vecx = vecx / den;
						vecy = vecy / den;
						vecx = vecx * 8;
						vecy = vecy * 8;
						var tempx = vecx;
						if(tempx < 0)
							tempx = tempx * (-1);
						var tempy = vecy;
						if(tempy < 0)
							tempy = tempy * (-1);
						if(tempx > tempy)
							tempen.style.left = tempen.offsetLeft + vecx + "px";
						else if(tempx <= tempy)
							tempen.style.top = tempen.offsetTop + vecy + "px";
						if(vecx <= 0)
						{
							document.getElementById("Spider" + i).style.top = "-20px";
							document.getElementById("Spider" + i).style.left = "0px";
						}
						else if(vecx > 0)
						{
							document.getElementById("Spider" + i).style.top = "-82px";
							document.getElementById("Spider" + i).style.left = "-75px";
						}
					}
				}
			}
		}
		Checkenemy();
	}
}

function CreateEnemy()
{
	if(level == 1)
	{
		for(var i = 0; i < 3; i++)
		{
			var enemydiv = document.createElement("div");
			enemydiv.id = "Spid" + i;
			enemydiv.className = "Spi";
			if(i == 0)
			{
				enemydiv.style.left = "544px";
				enemydiv.style.top = "128px";
			}
			else if(i == 1)
			{
				enemydiv.style.left = "112px";
				enemydiv.style.top = "416px";
			}
			else if(i == 2)
			{
				enemydiv.style.left = "608px";
				enemydiv.style.top = "544px";	
			}
			document.getElementById("Game").appendChild(enemydiv);
			var enemyimg = document.createElement("img");
			enemyimg.id = "Spider" + i;
			enemyimg.className = "Spimg";
			enemyimg.src = "images/Spider_sprite.png";
			document.getElementById("Spid" + i).appendChild(enemyimg);
		}
		leftlim[0] = 512;
		leftlim[1] = 48;
		leftlim[2] = 544;
		rightlim[0] = 720;
		rightlim[1] = 256;
		rightlim[2] = 832;
		state[0] = 1;
		state[1] = 1;
		state[2] = 1;
		dx[0] = 16;
		dx[1] = 16;
		dx[2] = 0;
		dy[0] = 0;
		dy[1] = 0;
		dy[2] = 16;
	}
	enemyint = setInterval(MoveEnemy, 50);
	stateint = setInterval(Setstate, 2000);	
}

function Moveloop()
{
	//if( (bombcnt == 1) && (is_pause == 0) )
	if(is_pause == 0)
	{
		var dugdiv = document.getElementById("Doug");
		var gamediv = document.getElementById("Game");
		if(dugdiv != null)
		{
			var templeft = dugdiv.offsetLeft;
			var temptop = dugdiv.offsetTop;
			if(moveleft == 1)
			{
				templeft = templeft - 16;
				var colreck = 0;
				if(level == 1)
				{
					for(var i = 0; i < 2; i++)
					{
						var rockdiv = document.getElementById("Rock" + i);
						if(rockdiv != null)
						{
							if( (templeft < (rockdiv.offsetLeft + rockdiv.clientWidth) ) && (templeft > rockdiv.offsetLeft) && (temptop < rockdiv.offsetTop) && ( (temptop + dugdiv.clientHeight) > rockdiv.offsetTop) )
								colreck = 1;
							if( (templeft < (rockdiv.offsetLeft + rockdiv.clientWidth) ) && (templeft > rockdiv.offsetLeft) && (temptop < (rockdiv.offsetTop + rockdiv.clientHeight) ) && ( (temptop + dugdiv.clientHeight) > (rockdiv.offsetTop + rockdiv.clientHeight) ) )
								colreck = 1;
							if( ( (templeft + 16) == (rockdiv.offsetLeft + rockdiv.clientWidth) ) && (temptop == rockdiv.offsetTop) )
								colreck = 1;
						}
					}
				}
				if( (templeft >= 0) && (colreck == 0) )
					dugdiv.style.left = templeft + "px";	
			}
			else if(moveright == 1)
			{
				templeft = templeft + 16;
				var colreck = 0;
				if(level == 1)
				{
					for(var i = 0; i< 2; i++)
					{
						var rockdiv = document.getElementById("Rock" + i);
						if(rockdiv != null)
						{
							if( ( (templeft + dugdiv.clientWidth) > rockdiv.offsetLeft) && (templeft < rockdiv.offsetLeft) && (temptop < rockdiv.offsetTop) && ( (temptop + dugdiv.clientHeight) > rockdiv.offsetTop) )
								colreck = 1;
							if( ( (templeft + dugdiv.clientWidth) > rockdiv.offsetLeft) && (templeft < rockdiv.offsetLeft) && (temptop < (rockdiv.offsetTop + rockdiv.clientHeight) ) && ( (temptop + dugdiv.clientHeight) > (rockdiv.offsetTop + rockdiv.clientHeight) ) )
								colreck = 1;
							if( ( (templeft + dugdiv.clientWidth - 16) == rockdiv.offsetLeft) && (temptop == rockdiv.offsetTop) )
								colreck = 1;
						}
					}
				}
				if( ( (templeft + dugdiv.clientWidth) <= gamediv.clientWidth) && (colreck == 0) )
					dugdiv.style.left = templeft + "px";
			}
			else if(moveup == 1)
			{
				temptop = temptop - 16;
				var colreck = 0;
				if(level == 1)
				{
					for(var i = 0; i < 2; i++)
					{
						var rockdiv = document.getElementById("Rock" + i);
						if(rockdiv != null)
						{
							if( (temptop > rockdiv.offsetTop) && (temptop < (rockdiv.offsetTop + rockdiv.clientHeight) ) && (templeft < rockdiv.offsetLeft) && ( (templeft + dugdiv.clientWidth) > rockdiv.offsetLeft) )
								colreck = 1;
							if( (temptop > rockdiv.offsetTop) && (temptop < (rockdiv.offsetTop + rockdiv.clientHeight) ) && (templeft < (rockdiv.offsetLeft + rockdiv.clientWidth) ) && ( (templeft + dugdiv.clientWidth) > (rockdiv.offsetLeft + rockdiv.clientWidth) ) )
								colreck = 1;
							if( ( (temptop + 16) == (rockdiv.offsetTop + rockdiv.clientHeight) ) && (templeft == rockdiv.offsetLeft) )
								colreck = 1;
						}
					}
				}
				if( (temptop >= 0) && (colreck == 0) )
					dugdiv.style.top = temptop + "px";
			}
			else if(movedown == 1)
			{
				temptop = temptop + 16;
				var colreck = 0;
				if(level == 1)
				{
					for(var i = 0; i < 2; i++)
					{
						var rockdiv = document.getElementById("Rock" + i);
						if(rockdiv != null)
						{
							if( ( (temptop + dugdiv.clientHeight) > rockdiv.offsetTop) && (temptop < rockdiv.offsetTop) && (templeft < rockdiv.offsetLeft) && ( (templeft + dugdiv.clientWidth) > rockdiv.offsetLeft) )
								colreck = 1;
							if( ( (temptop + dugdiv.clientHeight) > rockdiv.offsetTop) && (temptop < rockdiv.offsetTop) && (templeft < (rockdiv.offsetLeft + rockdiv.clientWidth) ) && ( (templeft + dugdiv.clientWidth) > (rockdiv.offsetLeft + rockdiv.clientWidth) ) )
								colreck = 1;
							if( ( (temptop + dugdiv.clientHeight - 16) == rockdiv.offsetTop) && (templeft == rockdiv.offsetLeft) )
								colreck = 1;
						}
					}
 				}
				if( ( (temptop + dugdiv.clientHeight) <= gamediv.clientHeight) && (colreck == 0) )
					dugdiv.style.top = temptop + "px";	
			}
			Checkcol();
		}
	}
}

function Createdoug()
{
	var dugdiv = document.createElement("div");
	dugdiv.id = "Doug";
	document.getElementById("Game").appendChild(dugdiv);
	var dugimg = document.createElement("img");
	dugimg.id = "Dig";
	dugimg.src = "images/Doug.png";
	document.getElementById("Doug").appendChild(dugimg);
	digint = setInterval(Moveloop, 50);
}

function Rockfall()
{
	if(level == 1)
	{
		for(var t = 0; t < 2; t++)
		{
			var rockdiv = document.getElementById("Rock" + t);
			var chefall = 3;
			if(rockdiv != null)
			{
				for( var j = 0; j < 51; j++)
				{
					for( var i = 0; i < 48; i++)
					{
						var tilediv = document.getElementById(i + "tile" + j);
						if(tilediv != null)
						{
							if( tilediv.offsetTop == (rockdiv.offsetTop + rockdiv.clientHeight) )
							{
								if(tilediv.offsetLeft == rockdiv.offsetLeft)
									chefall = chefall - 1;
								else if(tilediv.offsetLeft == (rockdiv.offsetLeft + 16) )
									chefall = chefall - 1;
								else if(tilediv.offsetLeft == (rockdiv.offsetLeft + 32) )
									chefall = chefall - 1;	
							}
						}
					}
				}
				if(chefall == 3)
				{
					rockdiv.style.top = rockdiv.offsetTop + 16 + "px";	
				}
				else if( (chefall == 0) && (roc[t] == 1) )
				{
					var crackaudio = new Audio("sounds/RockDrop.wav");
					crackaudio.play();
					document.getElementById("Rimg" + t).style.left = "-67px";
					var tempdiv = rockdiv;
					setTimeout(function() 
					{
						document.getElementById("Game").removeChild(tempdiv);		
					}, 2000);
					roc[t] = 0;
					console.log(t);
				}
				for(var k = 0; k < 3; k++)
				{
					var spidiv = document.getElementById("Spid" + k);
					if(spidiv != null)
					{
						if( ( (rockdiv.offsetTop < spidiv.offsetTop) && ( (rockdiv.offsetTop + rockdiv.clientHeight) >= spidiv.offsetTop) ) || ( ( (rockdiv.offsetTop + rockdiv.clientHeight) > (     spidiv.offsetTop + spidiv.clientHeight) ) && ( rockdiv.offsetTop <= (spidiv.offsetTop + spidiv.clientHeight) ) ) || ( (rockdiv.offsetTop >= spidiv.offsetTop) && ( (rockdiv.offsetTop + rockdiv.clientHeight) <= (spidiv.offsetTop + spidiv.clientHeight) ) ) )
						{
							if( ( (rockdiv.offsetLeft < spidiv.offsetLeft) && ( (rockdiv.offsetLeft + rockdiv.clientWidth) >= spidiv.offsetLeft) ) || ( ( (rockdiv.offsetLeft + rockdiv.clientWidth) > (spidiv.offsetLeft + spidiv.clientWidth) ) && ( rockdiv.offsetLeft <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) || ( (rockdiv.offsetLeft >= spidiv.offsetLeft) && ( (rockdiv.offsetLeft + rockdiv.clientWidth) <= (spidiv.offsetLeft + spidiv.clientWidth) ) ) )
							{
								if(chefall == 3)
								{
									document.getElementById("Game").removeChild(spidiv);
									var enemyaudio = new Audio("sounds/EnemyDeath.wav");
									enemyaudio.play();
									score = score + 10;
									document.getElementById("Score").innerHTML = "Score : " + score;
									roc[t] = 1;
								}
							}
						}
					}
				}
				var dugdiv = document.getElementById("Doug");
				if(dugdiv != null)
				{
					if(dugdiv.offsetTop == rockdiv.offsetTop)
					{
						if( (dugdiv.offsetLeft <= (rockdiv.offsetLeft + 20) ) && ( (dugdiv.offsetLeft + dugdiv.clientWidth) >= (rockdiv.offsetLeft + rockdiv.clientWidth - 20) ) )
						{
							document.getElementById("Game").removeChild(dugdiv);
							var deathaudio = new Audio("sounds/DougDeath.wav");
							deathaudio.play();
							setTimeout(Resetgame, 1000);	
						}
					}
				}
			}
		}
	}
}

function Createrocks()
{
	if(level == 1)
	{
		for(var i = 0; i < 2; i++)
		{
			var rockdiv = document.createElement("div");
			rockdiv.id = "Rock" + i;
			rockdiv.className = "Rck";
			if(i == 0)
			{
				rockdiv.style.left = "608px";
				rockdiv.style.top = "384px";
			}
			else if(i == 1)
			{
				rockdiv.style.left = "144px";
				rockdiv.style.top = "208px";	
			}
			document.getElementById("Game").appendChild(rockdiv);
			var rockimg = document.createElement("img");
			rockimg.id = "Rimg" + i;
			rockimg.className = "Rim";
			rockimg.src = "images/Rock.png";
			document.getElementById("Rock" + i).appendChild(rockimg);
			roc[i] = 0;
		}
	}
	fallint = setInterval(Rockfall, 50); 
}

function Createtiles()
{
	for( var b1 = 0; b1 < 48; b1++)
	{
		for( var b2 = 0; b2 < 2; b2++)
		{
			var bluediv = document.createElement("div");
			bluediv.id = b1 + "blue" + b2;
			bluediv.className = "til";
			bluediv.style.left = ( b1 * 16 ) + "px";
			bluediv.style.top = ( b2 * 16 ) + "px";
			document.getElementById("Game").appendChild(bluediv);
			var blueimg = document.createElement("img");
			blueimg.id = b1 + "bluimg" + b2;
			blueimg.src = "images/sky_16.png";
			document.getElementById(b1 + "blue" + b2).appendChild(blueimg);
		}
	}
	for( var g1 = 0; g1 < 48; g1++)
	{
		var grasdiv = document.createElement("div");
		grasdiv.id = "gras" + g1;
		grasdiv.className = "til";
		grasdiv.style.left = ( g1 * 16 ) + "px";
		grasdiv.style.top = 32 + "px";
		document.getElementById("Game").appendChild(grasdiv);
		var grasimg = document.createElement("img");
		grasimg.id = "grasimg" + g1;
		grasimg.src = "images/grass_16.png";
		document.getElementById("gras" + g1).appendChild(grasimg); 
	}
	for( var j = 0; j < 51; j++)
	{
		for( var i = 0; i < 48; i++)
		{
			var tilediv = document.createElement("div");
			tilediv.id = i + "tile" + j;
			tilediv.className = "til";
			tilediv.style.left = ( i * 16 ) + "px";
			tilediv.style.top = 48 + ( j * 16 ) + "px";
			document.getElementById("Game").appendChild(tilediv);
			var tileimg = document.createElement("img");
			tileimg.id = i + "tileimg" + j;
			if ( j < 15 )
				tileimg.src = "images/Dirt_1.jpg";
			else if ( j < 34 )
				tileimg.src = "images/Dirt_3.jpg";
			else if ( j < 51 )
				tileimg.src = "images/Dirt_2.jpg";
			document.getElementById(i + "tile" + j).appendChild(tileimg);
		}
	}
	if(level == 1)
	{
		for(var l = 32; l < 45; l++)
		{
			for(var m = 5; m < 8; m++)
			{
				var remtil = document.getElementById(l + "tile" + m);
				document.getElementById("Game").removeChild(remtil);
			}
		}
		for(var l = 38; l < 42; l++)
		{
			for(var m = 31; m < 47; m++)
			{
				var remtil = document.getElementById(l + "tile" + m);
				document.getElementById("Game").removeChild(remtil);	
			}
		}
		for(var l = 3; l < 16; l++)
		{
			for(var m = 23; m < 26; m++)
			{
				var remtil = document.getElementById(l + "tile" + m);
				document.getElementById("Game").removeChild(remtil);
			}
		}
		for(var l = 38; l < 41; l++)
		{
			for(var m = 21; m < 24; m++)
			{
				var remtil = document.getElementById(l + "tile" + m);
				document.getElementById("Game").removeChild(remtil);
			}
		}
		for(var l = 9; l< 12; l++)
		{
			for(var m = 10; m < 13; m++)
			{
				var remtil = document.getElementById(l + "tile" + m);
				document.getElementById("Game").removeChild(remtil);
			}
		}
	}
}

function Createlevel()
{
	var scorediv = document.createElement("div");
	scorediv.id = "Score";
	scorediv.innerHTML = "Score : ";
	document.getElementById("Game").appendChild(scorediv);
	Createtiles();
	Createdoug();
	CreateEnemy();
	Createrocks();
}

function Creategame()
{
	var prediv = document.getElementById("Preloader");
	document.getElementById("Background").removeChild(prediv);
	var gameaudio = new Audio("sounds/DigDougTheme.wav");
	gameaudio.loop = true;
	gameaudio.play();
	var gamediv = document.getElementById("Game");
	gamediv.style.visibility = "visible";
	var backdiv = document.getElementById("Background");
	var posx = backdiv.clientWidth/2 - gamediv.clientWidth/2;
	gamediv.style.left = posx + "px";
	var maindiv = document.createElement("div");
	maindiv.id = "Main";
	maindiv.style.width = gamediv.clientWidth + "px";
	maindiv.style.height = gamediv.clientHeight + "px";
	document.getElementById("Game").appendChild(maindiv);
	var mainimg = document.createElement("img");
	mainimg.id = "Mimg";
	mainimg.src = "images/Main_menu.png";
	document.getElementById("Main").appendChild(mainimg); 
}

function Checkbombtile(expdiv)
{
	for( var j = 0; j < 51; j++)
	{
		for( var i = 0; i < 48; i++)
		{
			var tilediv = document.getElementById(i + "tile" + j);
			if(tilediv != null)
			{
				if( (tilediv.offsetLeft >= expdiv.offsetLeft) && (tilediv.offsetLeft < ( expdiv.offsetLeft + expdiv.clientWidth) ) && (tilediv.offsetTop >= expdiv.offsetTop) && (tilediv.offsetTop < ( expdiv.offsetTop + expdiv.clientHeight) ) )
				{
					return 1;
				}
			}
		}
	}
	return 0;		
}

function Explode2()
{
	var bombimg = document.getElementById("Bimg1");
	var dugdiv = document.getElementById("Doug");
	if(bombimg != null)
	{
		setTimeout(function()
		{
			if(dugdiv != null)
			{
				bombimg.style.left = -48 + "px";
				setTimeout(function()
				{
					if(dugdiv != null)
					{
						bombimg.style.left = 0 + "px";
						bombimg.style.top = -50 + "px";
						setTimeout(function()
						{
							if(dugdiv != null)
							{
								bombdiv = document.getElementById("Bomb1");
								var bombaudio = new Audio("sounds/Explosion.wav");
								bombaudio.play();
								for(var i = 0; i < 5; i++)
								{
									var expdiv = document.createElement("div");
									expdiv.id = "Exp1" + i;
									expdiv.className = "ex";
									if(i == 0)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 1)
									{
										expdiv.style.left = bombdiv.offsetLeft + bombdiv.clientWidth + "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 2)
									{
										expdiv.style.left = bombdiv.offsetLeft - bombdiv.clientWidth+ "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 3)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop + bombdiv.clientHeight + "px";
									}
									else if(i == 4)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop - bombdiv.clientHeight + "px";
									}
									document.getElementById("Game").appendChild(expdiv);
									var bombtilecol = Checkbombtile(expdiv);
									if(bombtilecol == 0)
									{
										var expimg = document.createElement("img");
										expimg.id = "Expimg1" + i;
										expimg.className = "expim";
										if(i == 0)
											expimg.src = "images/Bomb_Explosion.png";
										else if(i == 1)
										{
											expimg.src = "images/End_Flames.png";
											expimg.style.left = "-54px";
										}
										else if(i == 2)
										{
											expimg.src = "images/End_Flames.png";
											expimg.style.left = "4px";
										}
										else if(i == 3)
										{
											expimg.src = "images/End_Flames_vert.png";
											expimg.style.top = "-54px";
											expimg.style.left = "-3px";
										}
										else if(i == 4)
										{
											expimg.src = "images/End_Flames_vert.png";
											expimg.style.left = "-2px";
											expimg.style.top = "4px";
										}
										document.getElementById("Exp1" + i).appendChild(expimg);
									}
									else if(bombtilecol == 1)
										document.getElementById("Game").removeChild(expdiv);
								}
								setTimeout(function()
								{
									if(dugdiv != null)
									{
										document.getElementById("Game").removeChild(bombdiv);
										for(var j = 0; j < 5; j++)
										{
											var expdiv = document.getElementById("Exp1" + j);
											if(expdiv != null)
												document.getElementById("Game").removeChild(expdiv);
											is_bomb2 = 0;
										}
									}
								},150);
							}
						},150);
					}
				},150);
			}
		}, 150);
	}
}


function Explodebomb()
{
	var bombimg = document.getElementById("Bimg");
	var dugdiv = document.getElementById("Doug");
	if(bombimg != null)
	{
		setTimeout(function()
		{
			if(dugdiv != null)
			{
				bombimg.style.left = -48 + "px";
				setTimeout(function()
				{
					if(dugdiv != null)
					{
						bombimg.style.left = 0 + "px";
						bombimg.style.top = -50 + "px";
						setTimeout(function()
						{
							if(dugdiv != null)
							{
								bombdiv = document.getElementById("Bomb");
								var bombaudio = new Audio("sounds/Explosion.wav");
								bombaudio.play();
								for(var i = 0; i < 5; i++)
								{
									var expdiv = document.createElement("div");
									expdiv.id = "Exp" + i;
									expdiv.className = "ex";
									if(i == 0)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 1)
									{
										expdiv.style.left = bombdiv.offsetLeft + bombdiv.clientWidth + "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 2)
									{
										expdiv.style.left = bombdiv.offsetLeft - bombdiv.clientWidth+ "px";
										expdiv.style.top = bombdiv.offsetTop + "px";
									}
									else if(i == 3)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop + bombdiv.clientHeight + "px";
									}
									else if(i == 4)
									{
										expdiv.style.left = bombdiv.offsetLeft + "px";
										expdiv.style.top = bombdiv.offsetTop - bombdiv.clientHeight + "px";
									}
									document.getElementById("Game").appendChild(expdiv);
									var bombtilecol = Checkbombtile(expdiv);
									if(bombtilecol == 0)
									{
										var expimg = document.createElement("img");
										expimg.id = "Expimg" + i;
										expimg.className = "expim";
										if(i == 0)
											expimg.src = "images/Bomb_Explosion.png";
										else if(i == 1)
										{
											expimg.src = "images/End_Flames.png";
											expimg.style.left = "-54px";
										}
										else if(i == 2)
										{
											expimg.src = "images/End_Flames.png";
											expimg.style.left = "4px";
										}
										else if(i == 3)
										{
											expimg.src = "images/End_Flames_vert.png";
											expimg.style.top = "-54px";
											expimg.style.left = "-3px";
										}
										else if(i == 4)
										{
											expimg.src = "images/End_Flames_vert.png";
											expimg.style.left = "-2px";
											expimg.style.top = "4px";
										}
										document.getElementById("Exp" + i).appendChild(expimg);
									}
									else if(bombtilecol == 1)
										document.getElementById("Game").removeChild(expdiv);
								}
								setTimeout(function()
								{
									if(dugdiv != null)
									{
										document.getElementById("Game").removeChild(bombdiv);
										for(var j = 0; j < 5; j++)
										{
											var expdiv = document.getElementById("Exp" + j);
											if(expdiv != null)
												document.getElementById("Game").removeChild(expdiv);
											is_bomb1 = 0;
										}
									}
								},150);
							}
						},150);
					}
				},150);
			}
		},150);
	}
}

function Assetloaded()
{
	loadcount = loadcount + 1;
	if(loadcount > 11)
		Creategame();
	else
	{
		document.getElementById("Prenum").innerHTML = (8 * loadcount) + "%";
		document.getElementById("Prebar").style.width = (20 * loadcount) + "px";
	}
}

function Loadassets()
{
	var prediv = document.getElementById("Preloader");
	var backdiv = document.getElementById("Background");
	prediv.style.left = (backdiv.clientWidth/2 - prediv.clientWidth/2) + "px";
	prediv.style.top = (backdiv.clientHeight/2 - prediv.clientHeight/2) + "px";
	var img1 = new Image();
	img1.src = "images/Main_Menu.png";
	img1.onload = Assetloaded;
	var img2 = new Image();
	img2.src = "images/pause.png"
	img2.onload = Assetloaded;
	var img3 = new Image();
	img3.src = "images/Rock.png"
	img3.onload = Assetloaded;
	var img4 = new Image();
	img4.src = "images/Spider_sprite.png"
	img4.onload = Assetloaded;
	var img5 = new Image();
	img5.src = "images/Bomb_explosion.png"
	img5.onload = Assetloaded;
	var img6 = new Image();
	img6.src = "images/Bomb_Sprite_Sheet.png"
	img6.onload = Assetloaded;
	var img7 = new Image();
	img7.src = "images/Dirt_1.jpg"
	img7.onload = Assetloaded;
	var img8 = new Image();
	img8.src = "images/Dirt_2.jpg"
	img8.onload = Assetloaded;
	var img9 = new Image();
	img9.src = "images/Dirt_3.jpg"
	img9.onload = Assetloaded;
	var img10 = new Image();
	img10.src = "images/Doug.png"
	img10.onload = Assetloaded;
	var img11 = new Image();
	img11.src = "images/End_Flames.png"
	img11.onload = Assetloaded;
	var img12 = new Image();
	img12.src = "images/End_Flames_vert.png"
	img12.onload = Assetloaded;
}

document.addEventListener('keydown', function(event) 
{
	var dugdiv = document.getElementById("Doug");
	if(dugdiv != null)
	{
		if(event.keyCode == 65)
		{
			if(is_pause == 1)
			{
				is_pause = 0;
				var pausediv = document.getElementById("Paus");
				document.getElementById("Game").removeChild(pausediv);
			}
			moveleft = 1;
			document.getElementById("Dig").style.left = "-189px";
		}
		else if(event.keyCode == 68)
		{
			if(is_pause == 1)
			{
				is_pause = 0;
				var pausediv = document.getElementById("Paus");
				document.getElementById("Game").removeChild(pausediv);
			}
			moveright = 1;
			document.getElementById("Dig").style.left = "-105px";
		}
		else if(event.keyCode == 87)
		{
			if(is_pause == 1)
			{
				is_pause = 0;
				var pausediv = document.getElementById("Paus");
				document.getElementById("Game").removeChild(pausediv);
			}
			moveup = 1;
			document.getElementById("Dig").style.left = "-57px";
		}
		else if(event.keyCode == 83)
		{
			if(is_pause == 1)
			{
				is_pause = 0;
				var pausediv = document.getElementById("Paus");
				document.getElementById("Game").removeChild(pausediv);
			}
			movedown = 1;
			document.getElementById("Dig").style.left = "-57px";
		}
		else if(event.keyCode == 32)
		{
			if(is_pause == 0)
			{
				/*if(isbomb == 0)
				{
					var dugdiv = document.getElementById("Doug");
					var bombdiv = document.createElement("div");
					bombdiv.id = "Bomb";
					var templeft = dugdiv.offsetLeft;
					var temptop = dugdiv.offsetTop ;
					bombdiv.style.left = templeft + "px";
					bombdiv.style.top = temptop + "px";
					document.getElementById("Game").appendChild(bombdiv);
					var bombimg = document.createElement("img");
					bombimg.id = "Bimg";
					bombimg.src = "images/Bomb_Sprite_Sheet.png";
					bombimg.style.backgroundPosition = "0 0";
					document.getElementById("Bomb").appendChild(bombimg);
					isbomb = 1;
				}
				else if(isbomb == 1)
				{
					var dugdiv = document.getElementById("Doug");
					var bombdiv = document.getElementById("Bomb");
					if( (dugdiv.offsetLeft > (bombdiv.offsetLeft - 150) ) && (dugdiv.offsetLeft < (bombdiv.offsetLeft + bombdiv.clientWidth + 150) ) )
					{
						if( (dugdiv.offsetTop > (bombdiv.offsetTop - 150) ) && (dugdiv.offsetTop < (bombdiv.offsetTop + bombdiv.clientHeight + 150) ) )
						{
							if(bombcnt == 1)
							{
								bombcnt = bombcnt - 1;
								Explodebomb();
							}
						}
					}
				}*/
				if( (bombcnt == 2) && (is_bomb1 == 0) )
				{
					is_bomb1 = 1;
					var dropaudio = new Audio("sounds/BombDrop.wav");
					dropaudio.play()
					var dugdiv = document.getElementById("Doug");
					var bombdiv = document.createElement("div");
					bombdiv.id = "Bomb";
					var templeft = dugdiv.offsetLeft;
					var temptop = dugdiv.offsetTop ;
					bombdiv.style.left = templeft + "px";
					bombdiv.style.top = temptop + "px";
					document.getElementById("Game").appendChild(bombdiv);
					var bombimg = document.createElement("img");
					bombimg.id = "Bimg";
					bombimg.src = "images/Bomb_Sprite_Sheet.png";
					bombimg.style.backgroundPosition = "0 0";
					document.getElementById("Bomb").appendChild(bombimg);
					bombcnt = bombcnt - 1;
					setTimeout(function()
					{
						var dugdiv = document.getElementById("Doug");
						var bombdiv = document.getElementById("Bomb");
						Explodebomb();
					}, 2000);
				}
				else if( (bombcnt == 1) && (is_bomb2 == 0) )
				{
					is_bomb2 = 1;
					var dropaudio = new Audio("sounds/BombDrop.wav");
					dropaudio.play()
					var dugdiv = document.getElementById("Doug");
					var bombdiv = document.createElement("div");
					bombdiv.id = "Bomb1";
					var templeft = dugdiv.offsetLeft;
					var temptop = dugdiv.offsetTop ;
					bombdiv.style.left = templeft + "px";
					bombdiv.style.top = temptop + "px";
					document.getElementById("Game").appendChild(bombdiv);
					var bombimg = document.createElement("img");
					bombimg.id = "Bimg1";
					bombimg.src = "images/Bomb_Sprite_Sheet.png";
					bombimg.style.backgroundPosition = "0 0";
					document.getElementById("Bomb1").appendChild(bombimg);
					bombcnt = bombcnt + 1;
					setTimeout(function()
					{
						var dugdiv = document.getElementById("Doug");
						var bombdiv = document.getElementById("Bomb1");
						Explode2();
					}, 2000);
				}
			}
		}
		else if(event.keyCode == 80)
		{
			if(is_pause == 0)
			{
				is_pause = 1;
				var gamediv = document.getElementById("Game");
				var pausediv = document.createElement("div");
				pausediv.id = "Paus";
				pausediv.style.left = "297px";
				pausediv.style.top = "399px";
				document.getElementById("Game").appendChild(pausediv);
				var pauseimg = document.createElement("img");
				pauseimg.id = "Paim";
				pauseimg.src = "images/pause.png";
				document.getElementById("Paus").appendChild(pauseimg); 
			}
		}
	}
});

document.addEventListener('keyup', function(event)
{
	var dugdiv = document.getElementById("Doug");
	if(dugdiv != null)
	{
		if(event.keyCode == 65)
		{
			moveleft = 0;
			document.getElementById("Dig").style.left = "-236px";
		}
		else if(event.keyCode == 68)
		{
			moveright = 0;
			document.getElementById("Dig").style.left = "-236px";
		}
		else if(event.keyCode == 87)
		{
			moveup = 0;
			document.getElementById("Dig").style.left = "-236px";
		}
		else if(event.keyCode == 83)
		{
			movedown = 0;
			document.getElementById("Dig").style.left = "-236px";
		}
	}
	if(event.keyCode == 32)
	{
		var maindiv = document.getElementById("Main");
		if(maindiv != null)
		{
			Createlevel();
			document.getElementById("Game").removeChild(maindiv);
		}
	}
});