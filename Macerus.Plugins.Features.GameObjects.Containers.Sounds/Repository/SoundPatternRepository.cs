using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class SoundPatternRepository : ISoundPatternRepository
    {
        public SoundPatternRepository()
        {
            StartingNotes = new List<IStartingNote>
            {
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x03,0x718)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x05,0x0b,0x03,0x708)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x03,0x600)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x05,0x0b,0x03,0x6f1)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x08,0x0f,0x05,0x480)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x07,0x09,0x05,0x441)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x07,0x608)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0c,0x07,0x504)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x07,0x6a0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0e,0x07,0x601)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0e,0x02,0x500)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0e,0x02,0x482)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x08,0x03,0x247)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x03,0x6e0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x0c,0x03,0x683)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x06,0x565)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0e,0x0d,0x06,0x503)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x07,0x7a0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x02,0x00,0x00,0x00)), //-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x08,0x0f,0x07,0x6e0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x00,0x00,0x00)), //-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x01,0x700)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0c,0x00,0x00,0x00)), //-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x08,0x0f,0x05,0x600)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0c,0x0c,0x03,0x5c0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x05,0x0f,0x02,0x650)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x00,0x00,0x00)),//-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0e,0x01,0x700)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0b,0x01,0x6e1)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x07,0x7c0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x0c,0x07,0x781)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x08,0x0f,0x07,0x680)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0a,0x0e,0x07,0x682)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0f,0x07,0x7a0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x00,0x00,0x00)), //-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0c,0x0f,0x02,0x440)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0b,0x0d,0x02,0x438)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x06,0x5c0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0e,0x0c,0x06,0x4b1)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x08,0x0e,0x04,0x790)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0a,0x0c,0x04,0x771)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x03,0x780)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0c,0x03,0x701)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0d,0x07,0x780)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0c,0x07,0x753)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x07,0x500)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0e,0x0d,0x07,0x481)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0a,0x0f,0x05,0x680)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x09,0x0d,0x05,0x631)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x07,0x0d,0x02,0x740)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x02,0x0c,0x02,0x701)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0f,0x07,0x740)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0c,0x07,0x701)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0f,0x07,0x6c0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x07,0x0e,0x06,0x681)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x07,0x0d,0x06,0x7e1)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0c,0x03,0x7c9)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x00,0x705)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0b,0x00,0x6c3)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0f,0x02,0x600)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x00,0x00,0x01)), //-0x08
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x0f,0x04,0x641)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x04,0x0f,0x04,0x580)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0d,0x0f,0x01,0x511)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0c,0x0e,0x01,0x50c)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x0f,0x03,0x564)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x03,0x0d,0x03,0x560)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x02,0x03,0x00,0x381)), //-0x05
                new StartingNote(Channel.Pulse, new WaveInstruction(0x02,0x03,0x00,0x5b0)), //-0x06
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x07,0x7c0)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x09,0x07,0x781)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0f,0x07,0x680)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x0f,0x0b,0x07,0x641)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0f,0x04,0x740)),
                new StartingNote(Channel.Pulse, new WaveInstruction(0x06,0x0c,0x03,0x712)),

                new StartingNote(Channel.Noise, new WaveInstruction(0x03,0x0a,0x01,0x1c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x03,0x0a,0x02,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0c,0x0e,0x04,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x07,0x0d,0x06,0x5c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x08,0x0d,0x04,0x8c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x04,0x0d,0x03,0x5c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0c,0x0e,0x06,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x02,0x0f,0x02,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x02,0x0f,0x02,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0f,0x00,0x00,0x00)), //-0x08
                new StartingNote(Channel.Noise, new WaveInstruction(0x06,0x0d,0x02,0x1c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x02,0x06,0x01,0x32)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x03,0x0e,0x04,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x04,0x07,0x04,0x21)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x02,0x0f,0x02,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0a,0x0e,0x06,0x6c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0a,0x0e,0x06,0x5c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x08,0x0e,0x04,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x06,0x0e,0x03,0x5c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0d,0x0f,0x06,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0e,0x0f,0x07,0x7c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x06,0x0e,0x03,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x03,0x0e,0x02,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0a,0x0a,0x06,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0d,0x01,0x00,0x7c)),//-0x01
                new StartingNote(Channel.Noise, new WaveInstruction(0x06,0x0e,0x06,0x4c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x06,0x00,0x00,0x01)),//-0x08
                new StartingNote(Channel.Noise, new WaveInstruction(0x05,0x0c,0x04,0x46)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0e,0x0f,0x02,0x65)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x02,0x09,0x02,0x49)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x03,0x0f,0x02,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x0f,0x0e,0x04,0x3c)),
                new StartingNote(Channel.Noise, new WaveInstruction(0x08,0x0d,0x06,0x2c)),
            };

            DutyCycles = new List<IDutyCycle>
            {
                new DutyCycle(Channel.Noise, new WaveInstruction()),
                new DutyCycle(Channel.Pulse, new WaveInstruction()),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x00)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x05)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x0A)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x0F)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x11)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x15)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x1B)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x22)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x33)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x40)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x44)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x50)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x5A)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x5F)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x77)),                
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x79)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x81)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x88)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0x99)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xA5)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xC9)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xCC)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xEE)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xF0)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xF1)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xF4)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xF5)),
                new DutyCycle(Channel.Pulse, new WaveInstruction(0xFA)),
            };

            Patterns = new List<ISoundPattern>
            {
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,-0x01,0x02,0x80),
						new WaveInstructionTransform(-0x07,-0x05,-0x04,-0x40)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0a,0x01,0x02,0x80),
						new WaveInstructionTransform(-0x07,-0x05,-0x04,-0x40)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x02,0x02,0x160),
						new WaveInstructionTransform(-0x05,0x01,-0x03,-0x40),
						new WaveInstructionTransform(0x05,-0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,0x01,0x02,0x61),
						new WaveInstructionTransform(-0x04,-0x02,-0x03,-0x41),
						new WaveInstructionTransform(0x05,0x01,-0x01,-0x110)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x06,-0x01,-0x04,0x160),
						new WaveInstructionTransform(0x06,-0x01,0x00,-0x04)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x01,-0x04,0xe0),
						new WaveInstructionTransform(0x06,-0x02,0x00,-0x07)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,-0x01,-0x01,-0x08),
						new WaveInstructionTransform(0x00,-0x01,0x01,-0x10),
						new WaveInstructionTransform(0x00,-0x01,-0x03,-0x10),
						new WaveInstructionTransform(-0x01,0x01,-0x01,-0x20),
						new WaveInstructionTransform(-0x01,0x00,0x00,-0x20),
						new WaveInstructionTransform(0x04,0x01,-0x02,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,-0x02,-0x01,-0x02),
						new WaveInstructionTransform(0x00,-0x01,0x01,-0x11),
						new WaveInstructionTransform(-0x02,0x02,-0x03,-0x10),
						new WaveInstructionTransform(0x01,-0x01,-0x01,-0x1f),
						new WaveInstructionTransform(-0x01,0x01,0x00,-0x1f),
						new WaveInstructionTransform(0x04,0x01,-0x02,-0x21)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x01,-0x01,0x04),
						new WaveInstructionTransform(-0x04,-0x01,0x00,-0x04),
						new WaveInstructionTransform(0x08,0x00,-0x03,-0x80),
						new WaveInstructionTransform(-0x04,-0x01,0x00,0x04),
						new WaveInstructionTransform(-0x04,0x00,-0x01,-0x04),
						new WaveInstructionTransform(0x04,-0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x01,-0x01,0x02),
						new WaveInstructionTransform(-0x04,-0x01,0x00,-0x02),
						new WaveInstructionTransform(0x08,0x00,-0x03,-0x80),
						new WaveInstructionTransform(-0x04,-0x01,0x00,0x02),
						new WaveInstructionTransform(-0x04,0x00,-0x01,-0x01),
						new WaveInstructionTransform(0x04,-0x01,-0x01,-0x11)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,0x00,0x01,0x80),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x10),
						new WaveInstructionTransform(0x02,-0x03,-0x02,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x01,0x7f),
						new WaveInstructionTransform(0x00,-0x02,-0x01,-0x1f),
						new WaveInstructionTransform(0x02,-0x03,-0x01,-0x21)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x02,-0x01,-0x21),
						new WaveInstructionTransform(-0x0b,-0x01,0x00,0x1f),
						new WaveInstructionTransform(0x05,0x01,0x01,-0x3f),
						new WaveInstructionTransform(0x06,0x02,-0x01,0x1f),
						new WaveInstructionTransform(0x00,-0x04,0x00,-0x1e)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,-0x01,0x01,-0xa0),
						new WaveInstructionTransform(-0x07,-0x02,-0x03,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,-0x01,0x01,-0x81),
						new WaveInstructionTransform(-0x06,-0x01,-0x03,-0x01)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x01,-0x02,0x17),
						new WaveInstructionTransform(-0x07,-0x02,-0x02,-0x20),
						new WaveInstructionTransform(0x0c,-0x01,0x00,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x02,-0x02,0x18),
						new WaveInstructionTransform(-0x05,-0x02,-0x02,-0x21),
						new WaveInstructionTransform(0x0b,0x01,0x00,-0x1f)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x09,-0x01,-0x01,0x03),
						new WaveInstructionTransform(0x04,0x01,-0x02,-0x03),
						new WaveInstructionTransform(0x00,0x00,0x02,0x38),
						new WaveInstructionTransform(-0x06,-0x01,-0x03,-0x01),
						new WaveInstructionTransform(0x0b,0x01,-0x01,0x01)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0d,0x0a,0x0f,0x6a1),
						new WaveInstructionTransform(-0x09,-0x02,-0x01,0x01),
						new WaveInstructionTransform(0x04,-0x01,-0x02,-0x01),
						new WaveInstructionTransform(0x00,0x00,0x02,0x35),
						new WaveInstructionTransform(-0x06,0x01,-0x03,0x03),
						new WaveInstructionTransform(0x0b,0x02,-0x01,-0x02)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x02,-0x01,-0x01,0x05),
						new WaveInstructionTransform(-0x03,0x01,-0x02,-0x05),
						new WaveInstructionTransform(0x00,0x00,0x02,-0x10),
						new WaveInstructionTransform(0x00,-0x01,-0x03,-0x10),
						new WaveInstructionTransform(0x01,0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x0b,-0x05,0x00,0x18)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,0x0a,0x0f,0x6a1),
						new WaveInstructionTransform(-0x02,-0x02,-0x01,0x02),
						new WaveInstructionTransform(-0x03,-0x01,-0x02,-0x02),
						new WaveInstructionTransform(0x00,0x00,0x02,-0x10),
						new WaveInstructionTransform(0x00,0x01,-0x03,-0x0f),
						new WaveInstructionTransform(0x01,0x02,-0x01,-0x11),
						new WaveInstructionTransform(0x0b,-0x03,0x00,0x18)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x80),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x40),
						new WaveInstructionTransform(0x00,0x01,0x00,0x00),
						new WaveInstructionTransform(0x00,0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x80),
						new WaveInstructionTransform(0x00,0x02,0x00,0x01),
						new WaveInstructionTransform(0x00,-0x02,0x00,0x81),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x40),
						new WaveInstructionTransform(0x04,-0x01,0x00,-0x01)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x08,0x0f,0x09,0x701),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x81),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x41),
						new WaveInstructionTransform(0x00,0x01,0x00,0x00),
						new WaveInstructionTransform(0x00,0x01,0x00,0x41),
						new WaveInstructionTransform(0x04,-0x02,0x00,-0x81)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x06,-0x02,-0x03,0x38),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x08),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x08),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x08),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x10),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x08),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x08),
						new WaveInstructionTransform(0x06,0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x09,-0x01,-0x02,0x39),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x08),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x08),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x08),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x08),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x08),
						new WaveInstructionTransform(0x00,0x01,0x00,0x08),
						new WaveInstructionTransform(0x06,0x00,0x00,0x08)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x04,0x01,0x01,-0x4e),
						new WaveInstructionTransform(0x04,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x04,0x03,0x01,-0x12),
						new WaveInstructionTransform(0x01,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x01,0x02,0x01,0x30),
						new WaveInstructionTransform(0x04,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x04,0x01,0x01,-0x4e),
						new WaveInstructionTransform(0x04,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x04,0x03,0x01,-0x12),
						new WaveInstructionTransform(0x01,-0x02,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x01,0x0f,0x0a,0x651),
						new WaveInstructionTransform(0x04,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x04,0x01,0x01,-0x4d),
						new WaveInstructionTransform(0x03,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x03,0x03,0x01,-0x13),
						new WaveInstructionTransform(0x07,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x07,0x01,0x01,-0x0d),
						new WaveInstructionTransform(0x03,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x03,0x03,0x01,-0x13),
						new WaveInstructionTransform(-0x01,-0x02,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,0x01,0x01,0x80),
						new WaveInstructionTransform(-0x02,-0x06,0x00,-0x40),
						new WaveInstructionTransform(0x06,0x05,-0x01,-0x140)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,0x01,0x01,0x00),
						new WaveInstructionTransform(0x00,-0x06,0x00,-0x60),
						new WaveInstructionTransform(0x05,0x05,-0x01,-0xa0)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x08,-0x01,-0x01,0x02),
						new WaveInstructionTransform(-0x06,-0x03,-0x01,-0x142),
						new WaveInstructionTransform(-0x02,0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x00,-0x01,0x01,-0x10),
						new WaveInstructionTransform(0x04,0x01,-0x04,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,-0x01,-0x01),
						new WaveInstructionTransform(-0x06,-0x01,-0x01,-0x13f),
						new WaveInstructionTransform(-0x02,0x02,-0x01,-0x0f),
						new WaveInstructionTransform(0x02,-0x01,0x01,-0x11),
						new WaveInstructionTransform(0x02,-0x01,-0x04,-0x1f)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x06,0x00,0x00,-0x20),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x20),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x20),
						new WaveInstructionTransform(0x0e,-0x01,-0x06,-0x20),
						new WaveInstructionTransform(-0x0b,-0x01,0x06,0x140),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x0b,-0x01,-0x06,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x08,0x00,0x00,-0x20),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x20),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x20),
						new WaveInstructionTransform(0x0e,-0x01,-0x06,-0x20),
						new WaveInstructionTransform(-0x0b,-0x01,0x06,0x140),
						new WaveInstructionTransform(-0x02,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x0d,-0x01,-0x06,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,-0x01,-0x01,0x04),
						new WaveInstructionTransform(-0x04,-0x01,0x00,-0x04),
						new WaveInstructionTransform(0x0b,0x00,-0x03,-0x80),
						new WaveInstructionTransform(-0x07,-0x01,0x00,0x03),
						new WaveInstructionTransform(-0x06,0x00,-0x01,0x05),
						new WaveInstructionTransform(0x06,-0x01,-0x01,0x08)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,0x0a,0x0f,0x741),
						new WaveInstructionTransform(0x02,-0x02,-0x01,0x02),
						new WaveInstructionTransform(-0x04,-0x01,0x00,-0x02),
						new WaveInstructionTransform(0x09,0x01,-0x03,-0x7f),
						new WaveInstructionTransform(-0x06,-0x01,0x00,-0x01),
						new WaveInstructionTransform(-0x04,0x01,-0x01,0x0b),
						new WaveInstructionTransform(0x05,-0x01,-0x01,0x0c)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x03,-0x01,0x01,0x60),
						new WaveInstructionTransform(-0x0b,-0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x04,0x00,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x03,-0x01,0x04,0x60),
						new WaveInstructionTransform(-0x0b,-0x01,-0x04,-0x10),
						new WaveInstructionTransform(0x05,0x00,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x07,-0x01,-0x03,-0x04),
						new WaveInstructionTransform(-0x02,-0x01,-0x01,0x14),
						new WaveInstructionTransform(0x00,-0x02,0x00,0x10),
						new WaveInstructionTransform(0x00,0x01,0x00,0x10),
						new WaveInstructionTransform(0x02,-0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x07,0x00,-0x03,-0x04),
						new WaveInstructionTransform(-0x02,-0x01,-0x01,0x14),
						new WaveInstructionTransform(0x03,-0x02,0x00,0x10),
						new WaveInstructionTransform(-0x02,0x01,0x00,0x10),
						new WaveInstructionTransform(0x02,-0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x07,0x01,0x01,0x30),
						new WaveInstructionTransform(-0x07,-0x02,-0x04,0x18)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,-0x01,0x02,0x31),
						new WaveInstructionTransform(-0x07,-0x01,-0x05,0x15)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,-0x01,0x04,-0x80),
						new WaveInstructionTransform(-0x07,-0x01,-0x04,0x10),
						new WaveInstructionTransform(-0x04,-0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x08,-0x01,0x04,-0x80),
						new WaveInstructionTransform(-0x07,0x00,-0x04,0x11),
						new WaveInstructionTransform(-0x04,-0x01,-0x01,-0x11),
						new WaveInstructionTransform(0x01,0x01,0x00,-0x0f),
						new WaveInstructionTransform(0x04,-0x01,-0x01,-0x11)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x0b,0x01,-0x01,0x20),
						new WaveInstructionTransform(0x0b,-0x01,-0x04,-0x60)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x0a,-0x01,-0x01,0x1f),
						new WaveInstructionTransform(0x0a,0x01,-0x04,-0x61)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x08),
						new WaveInstructionTransform(-0x07,-0x03,-0x03,-0x88),
						new WaveInstructionTransform(0x07,-0x01,-0x02,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x08),
						new WaveInstructionTransform(-0x04,-0x01,-0x03,-0x88),
						new WaveInstructionTransform(0x05,0x01,-0x02,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x07,-0x01,-0x03,0x20),
						new WaveInstructionTransform(0x00,0x01,0x00,0x20),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x20),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x20),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x20),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x20),
						new WaveInstructionTransform(0x05,-0x01,-0x01,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x06,0x00,-0x03,0x21),
						new WaveInstructionTransform(0x00,0x01,0x00,0x1f),
						new WaveInstructionTransform(0x00,-0x03,0x00,0x20),
						new WaveInstructionTransform(0x00,0x01,0x00,0x21),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x21),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x20),
						new WaveInstructionTransform(0x05,-0x01,-0x01,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x08,0x01,0x03,0x20),
						new WaveInstructionTransform(0x00,-0x02,-0x04,-0x30)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x02,0x00,0x00,0x07),
						new WaveInstructionTransform(0x0b,0x01,0x05,0x39),
						new WaveInstructionTransform(0x00,-0x03,-0x05,-0x40)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x06,-0x01,-0x01,0x04),
						new WaveInstructionTransform(-0x06,-0x01,-0x01,0x0c),
						new WaveInstructionTransform(-0x02,-0x01,-0x02,0x10),
						new WaveInstructionTransform(-0x01,0x00,0x00,0x20),
						new WaveInstructionTransform(0x05,0x01,-0x02,0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,-0x01,-0x01,0x01),
						new WaveInstructionTransform(-0x05,-0x01,-0x01,0x0f),
						new WaveInstructionTransform(-0x02,-0x01,-0x02,0x10),
						new WaveInstructionTransform(-0x01,0x01,0x00,0x20),
						new WaveInstructionTransform(0x05,-0x01,-0x02,0x21)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,0x00,0x40),
						new WaveInstructionTransform(-0x0b,0x01,-0x03,-0x10),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x03,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x07,-0x01,-0x01,0x40),
						new WaveInstructionTransform(-0x0a,-0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x03,-0x10)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,-0x01,0x00,0x01),
						new WaveInstructionTransform(0x03,0x01,0x00,-0x01),
						new WaveInstructionTransform(-0x02,-0x01,0x00,-0x01),
						new WaveInstructionTransform(-0x02,-0x01,0x00,0x02),
						new WaveInstructionTransform(0x02,0x01,0x00,-0x01),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x01),
						new WaveInstructionTransform(0x02,-0x01,-0x05,-0x01)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x02),
						new WaveInstructionTransform(0x04,0x01,0x01,-0x04),
						new WaveInstructionTransform(-0x02,-0x01,0x00,0x04),
						new WaveInstructionTransform(-0x02,0x01,-0x01,0x02),
						new WaveInstructionTransform(0x09,-0x02,-0x01,-0x04)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x01,0x00,-0x05),
						new WaveInstructionTransform(-0x04,-0x03,0x04,0x10),
						new WaveInstructionTransform(-0x02,0x02,-0x01,-0x10),
						new WaveInstructionTransform(0x02,-0x02,-0x01,-0xe0),
						new WaveInstructionTransform(0x02,-0x01,-0x01,0x04)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x01,0x00,-0x02),
						new WaveInstructionTransform(-0x04,-0x02,0x04,0x11),
						new WaveInstructionTransform(-0x02,0x01,-0x01,-0x11),
						new WaveInstructionTransform(0x02,-0x01,-0x01,-0xe0),
						new WaveInstructionTransform(0x02,-0x02,-0x01,0x07)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x02,-0x01,-0x01,0x40)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x03,0x0c,0x0a,0x5c0),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x41),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x3f),
						new WaveInstructionTransform(0x00,0x01,0x00,0x41),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x40),
						new WaveInstructionTransform(0x00,-0x01,0x00,0x3f),
						new WaveInstructionTransform(0x00,0x01,0x00,0x41),
						new WaveInstructionTransform(0x02,-0x02,-0x01,0x3f)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0a,-0x02,0x02,0xe0),
						new WaveInstructionTransform(-0x05,0x02,-0x02,-0x08),
						new WaveInstructionTransform(0x00,-0x03,-0x03,0x01)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0a,-0x01,0x02,0x160),
						new WaveInstructionTransform(-0x06,-0x01,-0x01,-0x08),
						new WaveInstructionTransform(0x00,0x00,-0x04,0x04)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x04),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x04),
						new WaveInstructionTransform(-0x05,-0x01,0x00,0x00)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x04),
						new WaveInstructionTransform(0x02,-0x01,0x00,-0x04),
						new WaveInstructionTransform(-0x06,0x00,0x00,-0x02)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,-0x01,-0x01,-0x20),
						new WaveInstructionTransform(0x03,-0x01,-0x01,-0x22),
						new WaveInstructionTransform(-0x03,-0x02,0x01,-0x9e),
						new WaveInstructionTransform(0x06,0x02,-0x01,0x1e),
						new WaveInstructionTransform(-0x05,0x02,0x02,0x82),
						new WaveInstructionTransform(0x01,-0x01,0x01,-0x40),
						new WaveInstructionTransform(0x04,-0x01,-0x03,0x1e)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,-0x01,-0x01,-0x20),
						new WaveInstructionTransform(0x03,0x00,-0x01,-0x20),
						new WaveInstructionTransform(-0x03,-0x03,0x01,-0xa0),
						new WaveInstructionTransform(0x06,0x03,-0x01,0x20),
						new WaveInstructionTransform(-0x05,0x01,0x02,0x80),
						new WaveInstructionTransform(0x00,-0x01,0x01,-0x40),
						new WaveInstructionTransform(0x05,0x00,-0x03,0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,0x0c,0x0a,0x280),
						new WaveInstructionTransform(-0x06,-0x03,-0x03,-0x180),
						new WaveInstructionTransform(0x07,-0x03,-0x01,-0x100)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,0x0a,0x0b,0x1ad),
						new WaveInstructionTransform(-0x06,-0x02,-0x03,-0xad),
						new WaveInstructionTransform(0x07,-0x05,-0x01,-0x100)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x09,-0x01,-0x03,0x01),
						new WaveInstructionTransform(0x04,0x01,0x02,-0x01),
						new WaveInstructionTransform(-0x06,-0x02,-0x03,0x02),
						new WaveInstructionTransform(0x04,-0x01,-0x02,-0x02)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x09,-0x01,-0x03,-0x01),
						new WaveInstructionTransform(0x04,0x01,0x02,0x01),
						new WaveInstructionTransform(0x05,-0x01,-0x03,0x00)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x01,-0x01,0x04),
						new WaveInstructionTransform(0x05,-0x01,0x01,0x0c),
						new WaveInstructionTransform(-0x07,0x00,-0x02,0x00),
						new WaveInstructionTransform(-0x02,-0x01,-0x01,-0x08),
						new WaveInstructionTransform(-0x01,0x01,-0x01,-0x18),
						new WaveInstructionTransform(-0x01,0x00,0x00,-0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x02,-0x20)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x02,-0x01,0x01),
						new WaveInstructionTransform(0x05,0x01,0x01,0x0f),
						new WaveInstructionTransform(-0x07,0x00,-0x02,0x00),
						new WaveInstructionTransform(-0x02,-0x01,-0x01,-0x0a),
						new WaveInstructionTransform(-0x01,0x01,-0x01,-0x16),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x0f),
						new WaveInstructionTransform(0x04,-0x02,-0x02,-0x21)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,-0x01,-0x10),
						new WaveInstructionTransform(-0x0b,0x01,0x01,0x10),
						new WaveInstructionTransform(0x01,-0x04,-0x01,0x08),
						new WaveInstructionTransform(0x03,0x02,-0x02,0x08)
					}),
				new SoundPattern(
					Channel.Pulse,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,0x00,-0x0e),
						new WaveInstructionTransform(-0x0c,0x01,0x00,0x0e),
						new WaveInstructionTransform(0x01,0x00,0x00,0x0f),
						new WaveInstructionTransform(0x04,-0x01,-0x02,0x11)
					}),

				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,-0x01,0x03,0x10),
						new WaveInstructionTransform(-0x06,-0x01,-0x03,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,0x02,-0x10),
						new WaveInstructionTransform(-0x09,-0x01,-0x02,-0x10),
						new WaveInstructionTransform(0x05,-0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x02,-0x02,0x03,0x10),
						new WaveInstructionTransform(0x02,-0x01,-0x01,-0x10),
						new WaveInstructionTransform(0x03,-0x01,-0x04,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x01,0x01,0x00,-0x10),
						new WaveInstructionTransform(-0x04,-0x01,-0x02,0x10),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x10),
						new WaveInstructionTransform(0x03,-0x01,-0x01,0x00),
						new WaveInstructionTransform(0x01,-0x02,-0x02,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x04,0x01,-0x02,0x10),
						new WaveInstructionTransform(0x0b,-0x02,0x04,-0x10),
						new WaveInstructionTransform(-0x07,0x02,-0x02,0x20),
						new WaveInstructionTransform(0x07,-0x01,0x03,-0x10),
						new WaveInstructionTransform(0x00,0x02,-0x05,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0b,0x01,0x03,-0x10),
						new WaveInstructionTransform(-0x07,-0x03,-0x05,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,-0x01,0x01,0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x05,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x06,-0x01,0x02,0x02),
						new WaveInstructionTransform(0x07,-0x01,0x03,-0x02),
						new WaveInstructionTransform(-0x09,-0x01,-0x02,-0x01),
						new WaveInstructionTransform(0x00,0x02,-0x01,0x02),
						new WaveInstructionTransform(0x02,-0x03,0x02,-0x01),
						new WaveInstructionTransform(-0x02,0x02,-0x02,0x01),
						new WaveInstructionTransform(0x02,-0x01,-0x03,-0x02)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x06,-0x01,0x02,0x02),
						new WaveInstructionTransform(0x00,-0x01,0x03,-0x02),
						new WaveInstructionTransform(-0x03,-0x01,-0x02,-0x01),
						new WaveInstructionTransform(-0x02,0x01,-0x01,-0x0f),
						new WaveInstructionTransform(-0x01,-0x02,0x02,0x10),
						new WaveInstructionTransform(0x01,-0x01,-0x02,-0x10),
						new WaveInstructionTransform(0x05,-0x01,-0x03,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x0b,0x00,0x00,0x00),
						new WaveInstructionTransform(0x00,0x0d,0x09,0x4c),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x20),
						new WaveInstructionTransform(0x00,0x02,0x00,0x10),
						new WaveInstructionTransform(0x00,-0x02,0x00,0x00),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x10),
						new WaveInstructionTransform(0x04,-0x02,0x00,0x20)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x03,-0x02,-0x01,0x10),
						new WaveInstructionTransform(-0x01,0x01,0x01,0x00),
						new WaveInstructionTransform(0x01,-0x01,-0x01,0x10),
						new WaveInstructionTransform(-0x03,0x01,0x01,-0x10),
						new WaveInstructionTransform(0x03,-0x02,0x00,0x10),
						new WaveInstructionTransform(-0x02,0x02,0x00,-0x10),
						new WaveInstructionTransform(-0x02,-0x02,-0x01,0x10),
						new WaveInstructionTransform(0x04,0x02,0x01,-0x10),
						new WaveInstructionTransform(-0x05,-0x02,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,0x00,0x00,-0x11),
						new WaveInstructionTransform(0x06,0x00,0x00,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,0x02,-0x10),
						new WaveInstructionTransform(-0x08,0x01,-0x02,0x10),
						new WaveInstructionTransform(0x04,-0x03,0x03,0x20),
						new WaveInstructionTransform(0x07,0x01,-0x05,0x01)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,0x00,0x00,-0x11),
						new WaveInstructionTransform(0x00,0x00,-0x03,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x01,0x04,-0x12),
						new WaveInstructionTransform(-0x02,-0x01,0x01,0x00),
						new WaveInstructionTransform(0x02,0x00,-0x01,-0x0e),
						new WaveInstructionTransform(0x02,0x01,-0x01,0x10),
						new WaveInstructionTransform(0x04,-0x01,-0x03,0x01),
						new WaveInstructionTransform(-0x04,0x00,-0x01,-0x11)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,-0x01,-0x04,-0x10),
						new WaveInstructionTransform(-0x0c,-0x01,0x00,0x10),
						new WaveInstructionTransform(0x05,0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,-0x01,0x00,0x10),
						new WaveInstructionTransform(-0x06,-0x01,-0x04,-0x20),
						new WaveInstructionTransform(0x02,0x01,0x01,0x10),
						new WaveInstructionTransform(0x02,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x00,-0x01,-0x02,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x06,-0x02,0x00,-0x10),
						new WaveInstructionTransform(-0x06,0x01,-0x03,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x08,-0x01,0x03,-0x10),
						new WaveInstructionTransform(-0x08,-0x01,0x00,-0x10),
						new WaveInstructionTransform(-0x03,-0x01,-0x03,0x10),
						new WaveInstructionTransform(0x00,-0x01,-0x01,0x10),
						new WaveInstructionTransform(0x05,0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x09,-0x01,0x00,-0x10),
						new WaveInstructionTransform(0x0b,0x01,-0x04,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x02,0x00,-0x01,-0x10),
						new WaveInstructionTransform(-0x03,-0x01,-0x02,0x10),
						new WaveInstructionTransform(0x06,0x00,-0x02,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x02,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x01,0x01,0x01,0x00),
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x10),
						new WaveInstructionTransform(0x02,-0x01,0x00,0x10),
						new WaveInstructionTransform(0x02,0x01,-0x03,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,-0x01,0x04,0x10),
						new WaveInstructionTransform(-0x03,0x00,-0x02,-0x10),
						new WaveInstructionTransform(0x07,-0x01,0x03,0x10),
						new WaveInstructionTransform(-0x0a,0x02,-0x05,-0x10),
						new WaveInstructionTransform(0x06,-0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x01,-0x02,-0x10),
						new WaveInstructionTransform(-0x09,0x01,-0x01,0x10),
						new WaveInstructionTransform(0x03,-0x01,-0x02,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x00,0x0e,0x08,0x10),
						new WaveInstructionTransform(-0x01,-0x02,-0x01,-0x10),
						new WaveInstructionTransform(-0x04,-0x01,-0x02,-0x10),
						new WaveInstructionTransform(0x07,-0x01,-0x01,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x09,-0x01,0x00,-0x10),
						new WaveInstructionTransform(-0x05,-0x01,-0x01,0x0e),
						new WaveInstructionTransform(-0x09,-0x01,-0x03,0x11),
						new WaveInstructionTransform(0x0e,0x01,0x00,-0x0f)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,0x0e,0x0a,0x5b),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x10),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x10),
						new WaveInstructionTransform(0x00,0x01,0x00,-0x10),
						new WaveInstructionTransform(0x00,-0x02,0x00,-0x01),
						new WaveInstructionTransform(0x00,-0x01,0x00,-0x01),
						new WaveInstructionTransform(0x03,-0x01,-0x01,-0x02)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x08,-0x02,0x01,-0x02),
						new WaveInstructionTransform(-0x05,0x02,-0x01,0x01),
						new WaveInstructionTransform(0x00,-0x01,-0x03,-0x01)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x01,-0x01,0x00,-0x10),
						new WaveInstructionTransform(0x01,-0x01,0x00,0x01),
						new WaveInstructionTransform(-0x06,0x00,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x05,0x02,0x03,-0x20),
						new WaveInstructionTransform(-0x06,-0x01,-0x03,0x10),
						new WaveInstructionTransform(0x07,-0x01,-0x01,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x0a,-0x01,0x04,-0x10),
						new WaveInstructionTransform(0x02,-0x01,0x01,0x10),
						new WaveInstructionTransform(-0x07,-0x01,-0x06,-0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(-0x05,-0x02,0x03,0x10),
						new WaveInstructionTransform(0x00,0x00,0x00,-0x10),
						new WaveInstructionTransform(0x02,-0x01,0x00,0x10),
						new WaveInstructionTransform(0x03,-0x01,-0x05,0x10)
					}),
				new SoundPattern(
					Channel.Noise,
					new List<IWaveInstruction>()
					{
						new WaveInstructionTransform(0x04,-0x01,0x00,0x10),
						new WaveInstructionTransform(-0x02,-0x01,0x00,-0x10),
						new WaveInstructionTransform(-0x02,-0x02,-0x05,-0x10)
					}),
			};

			Lengths = new List<int>
			{
				0x01,
				0x80,
				0xc0,
				0x40,
				0x20,
				0x04,
				0xff,
				0xa0,
				0x10,
				0x08,
				0xe0,
				0x60,
				0x35,
				0xcc,
				0x90,
				0xdd,
				0x30,
				0x00,
				0xf0
			};
        }

        public IReadOnlyCollection<IStartingNote> StartingNotes { get; }

        public IReadOnlyCollection<IDutyCycle> DutyCycles { get; }

        public IReadOnlyCollection<ISoundPattern> Patterns { get; }

		public IReadOnlyCollection<int> Lengths { get; }
	}
}
